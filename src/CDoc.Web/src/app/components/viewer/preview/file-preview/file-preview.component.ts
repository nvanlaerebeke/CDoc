import { Component, OnInit, Input, SimpleChanges } from '@angular/core';
import { faDownload } from '@fortawesome/free-solid-svg-icons';
import { DocumentService } from 'src/app/services/document/document.service';

import { Item } from 'src/app/types/Item';

@Component({
  selector: 'app-file-preview[Item]',
  templateUrl: './file-preview.component.html',
  styleUrls: ['./file-preview.component.css']
})

export class FilePreviewComponent implements OnInit {
  @Input() Item!: Item;

  DownloadUrl!: URL;
  faDownload = faDownload;

  constructor(private documentService: DocumentService) {  }

  ngOnInit(): void { 
    this.DownloadUrl = this.documentService.getDataUrl(this.Item.repository, this.Item.path);
  }

  onChanges(changes: SimpleChanges) {
    if(changes['Item'].currentValue != changes['Item'].previousValue) {
      this.DownloadUrl = this.documentService.getDataUrl(this.Item.repository, this.Item.path);
    }
  }
}
