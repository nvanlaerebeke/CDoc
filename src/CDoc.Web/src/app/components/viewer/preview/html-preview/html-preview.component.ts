import { Component, OnInit, SimpleChanges, Input } from '@angular/core';
import { Item } from 'src/app/types/Item';
import { DocumentService } from 'src/app/services/document/document.service';

@Component({
  selector: 'app-html-preview[Item]',
  templateUrl: './html-preview.component.html',
  styleUrls: ['./html-preview.component.css']
})
export class HtmlPreviewComponent implements OnInit {
  @Input() Item!: Item;
  Html: string = "";

  constructor(private documentService: DocumentService) {  }

  ngOnInit(): void { }

  ngOnChanges(changes: SimpleChanges) {
    if(changes['Item'].currentValue != changes['Item'].previousValue) {
      this.documentService.getPreview(this.Item.repository, this.Item.path).subscribe(result => {
        this.Html = result.html;
      });
    }
  }
}
