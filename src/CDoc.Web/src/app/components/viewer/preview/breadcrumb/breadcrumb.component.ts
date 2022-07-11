import { Component, OnInit, Input, SimpleChanges } from '@angular/core';
import { Item } from '../../../../types/Item';

@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.css']
})

export class BreadcrumbComponent implements OnInit {
  @Input() Item!: Item;
  Parts: string[] = [];

  constructor() { 
  }

  ngOnInit(): void {
    this.Parts = this.Item.path.split('/').filter(p => p.trim() != "")
  }

  ngOnChanges(changes: SimpleChanges) {
    if(changes['Item'].currentValue != changes['Item'].previousValue) {
      this.Parts = this.Item.path.split('/').filter(p => p.trim() != "")
    }
  }
}
