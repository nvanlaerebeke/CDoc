import { Component, OnInit, Input } from '@angular/core';
import { faDownload } from '@fortawesome/free-solid-svg-icons';
import { Item } from 'src/app/types/Item';
import { DocumentService } from 'src/app/services/document/document.service';

@Component({
  selector: 'app-image-preview[Item]',
  templateUrl: './image-preview.component.html',
  styleUrls: ['./image-preview.component.css']
})

export class ImagePreviewComponent implements OnInit {
  @Input() Item!: Item;

  ImageSource!: URL;
  faDownload = faDownload;

  constructor(private documentService: DocumentService) {  }

  ngOnInit(): void {
    this.ImageSource = this.documentService.getDataUrl(this.Item.repository, this.Item.path);
  }
  
  //Because the reference to Item does not change, no change is detected
  //This is run on every check.
  ngDoCheck() {
    this.ImageSource = this.documentService.getDataUrl(this.Item.repository, this.Item.path);
  }
}
