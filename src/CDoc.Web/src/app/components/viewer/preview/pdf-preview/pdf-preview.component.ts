import { Component, OnInit, Input, SimpleChanges } from '@angular/core';
import { DocumentService } from 'src/app/services/document/document.service';
import { Item } from 'src/app/types/Item';

@Component({
  selector: 'app-pdf-preview',
  templateUrl: './pdf-preview.component.html',
  styleUrls: ['./pdf-preview.component.css']
})

export class PdfPreviewComponent implements OnInit {
  @Input() Item!: Item;
  Source!: URL;

  constructor(private documentService: DocumentService) {  }
  
  ngOnInit(): void { 
    this.Source = this.documentService.getDataUrl(this.Item.repository, this.Item.path);
  }

  onChanges(changes: SimpleChanges) {
    if(changes['Item'].currentValue != changes['Item'].previousValue) {
      this.Source = this.documentService.getDataUrl(this.Item.repository, this.Item.path);
    }
  }
}
