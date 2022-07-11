import { Component, OnInit, Input } from '@angular/core';
import { Item } from "../../../../types/Item";
import { faChevronDown, faChevronRight } from  "@fortawesome/free-solid-svg-icons"
import { IconProp } from '@fortawesome/fontawesome-svg-core';
import { MimeType } from 'src/app/types/MimeType';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tree-label[Item][CurrentItem]',
  templateUrl: './tree-label.component.html',
  styleUrls: ['./tree-label.component.css']
})

export class TreeLabelComponent implements OnInit {
  Link!: string;
  @Input() Item!: Item;
  @Input() CurrentItem?: Item;

  faChevronDown = faChevronDown;
  faChevronRight = faChevronRight;
  
  constructor(private router: Router) { }

  ngOnInit(): void { 
    this.Link = this.router.createUrlTree([ "view", this.Item.repository, this.Item.path]).toString();
  }

  getIcon(item: Item) : IconProp {
    return MimeType.getIcon(item.mimetype);
  }

  onChangeState(item: Item) {
    this.Item.isOpen = !item.isOpen;
  }
}
