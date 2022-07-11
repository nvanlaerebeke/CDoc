import { Component, OnInit, Input, Output, EventEmitter, SimpleChanges } from '@angular/core';
import { Item } from 'src/app/types/Item';
import { DocumentService } from 'src/app/services/document/document.service';
import { MimeType } from 'src/app/types/MimeType';
import { CDocCookieService } from 'src/app/services/config/cdoc.cookie.service';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tree[Repository][Path]',
  templateUrl: './tree.component.html',
  styleUrls: ['./tree.component.css']
})
export class TreeComponent implements OnInit {
  Items: Item[] = [];
  Display: string;
  
  @Input() Repository!: string;
  @Input() Path: string = '/';
  @Input() CurrentItem?: Item;

  constructor(
    private documentService: DocumentService, 
    private cookieService: CDocCookieService,
    private router: Router
  ) { 
    this.Display = this.cookieService.get("CDoc.Display") ?? "markdown";
  }

  ngOnInit(): void { 
    if(this.Repository != null && this.Items.length == 0) {
      this.loadTree();
    }
  }

  ngOnChanges(changes: SimpleChanges) : void {
    if(changes['Repository'] != undefined && changes['Repository'].currentValue != changes['Repository'].previousValue) {
      this.loadTree();
    }
  }

  ShouldBeDisplayed(item: Item, display: string) {
    if(item.mimetype == "Directory") {
      return false;
    }
    if(display == "all" || item.mimetype == "text/markdown") {
      return true;
    }

    if(display == "markdown") {
      return false;
    }
    return display == "documents" && MimeType.isDocument(item.mimetype);
  }

  ///
  /// Logic for loading and filtering the tree based on the selected view mode
  ///
  private loadTree() {
    this.documentService.list(this.Repository!, this.Path).subscribe(items => {
      this.Items = items;

      this.applyDisplay();
      this.openItemsInPath();
      
      //Only select a "default" page for the root
      if(
        this.Repository == null || 
        (this.Path != '' && this.Path != '/')
      ) {
        return;
      }

      var selectedItem = this.documentService.getDefaultItem(items);
      if(selectedItem != null && this.CurrentItem == undefined) {
        this.router.navigate(['view', selectedItem.repository, selectedItem.path]);
      }
    });

    this.cookieService.watch("CDoc.Display").subscribe(newValue => {
      if(this.Display != newValue) {
        this.Display = newValue;
        this.applyDisplay();
      }
    });
  }

  private applyDisplay() : void {
    this.Items.forEach(item => {
      //Set this to undefined, need to make sure the display filtering is re-done
      //Example: have a directory with only images and switch display to markdown only - the image directory will still be shown
      //There might be a more elegant solution to this
      //
      //For example:
      //  In the Item class returned from the Document service, add an observable to the cookie service
      //  When the display option changes, re-calculate a 'visible/shown' property in the object
      item.hasChildren = undefined; 
      if(item.mimetype == "Directory" && item.hasChildren === undefined) {
        this.itemHasChildren(item, this.Display, this.ShouldBeDisplayed).subscribe(result => {
          item.hasChildren = result;
        });
      }
    });
  }

  private itemHasChildren(item: Item, display: string, filter: (item: Item, display: string) => boolean): Observable<boolean> {
    return new Observable<boolean>((observer) => {
      if(item.mimetype != "Directory") {
        observer.next(false);
      }

      this.documentService.list(this.Repository!, item.path).subscribe(items => {
        for(let i = 0; i < items.length; i++) {
          if(filter(items[i], display)) {
            observer.next(true);
            return;
          }
        }

        var toDo = items;
        this.itemHasChildrenSubRequest(toDo, display, filter).subscribe(result => observer.next(result));
        return false;
      });
    });
  }

  private itemHasChildrenSubRequest(toDoItems: Item[], display: string, filter: (item: Item, display: string) => boolean): Observable<boolean> {
    var dirs: Item[] = [];
    for(let i = 0; i < toDoItems.length; i++) {
      if(toDoItems[i].mimetype == "Directory") {
        dirs.push(toDoItems[i]);
      }
    }

    return new Observable<boolean>((observer) => {
      if(dirs.length == 0) {
        observer.next(false);
        return;
      }

      var item = dirs.pop();
      this.itemHasChildren(item!, display, filter).subscribe(result => {
        if(result) {
          observer.next(true);
          return;
        } 

        this.itemHasChildrenSubRequest(dirs, display, filter).subscribe(result => {
          observer.next(result);
        });
      });
    });
  }

  private openItemsInPath() {
    for(let i = 0; i < this.Items.length; i++) {
      if(this.Items[i].mimetype == 'Directory' && !this.Items[i].isOpen && this.CurrentItem?.path.startsWith(this.Items[i].path)) {
        this.Items[i].isOpen = true;
      }
    }
  }
}