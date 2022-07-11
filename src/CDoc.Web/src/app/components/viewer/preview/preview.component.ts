import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Item } from "../../../types/Item";
import { MimeType } from 'src/app/types/MimeType';
import { ActivatedRoute } from '@angular/router';
import { NavigationInfo } from 'src/app/types/NavigationInfo';
@Component({
  selector: 'app-preview[Item]',
  templateUrl: './preview.component.html',
  styleUrls: ['./preview.component.css']
})

export class PreviewComponent implements OnInit {
  Repository?: string;
  Item?: Item;
  Previewer: string = "file";

  constructor(private route: ActivatedRoute) {  }

  ngOnInit(): void { 
    this.route.data.subscribe((navigationInfo: any) => {
      this.Repository = navigationInfo.data.Repository;
      this.Item = navigationInfo.data.Item;
    });
  }
}