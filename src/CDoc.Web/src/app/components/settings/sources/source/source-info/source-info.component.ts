import { Component, Input, OnInit } from '@angular/core';
import { faRotate } from '@fortawesome/free-solid-svg-icons';
import { SourceService } from 'src/app/services/source/source.service';
import { Source } from 'src/app/types/Source';

@Component({
  selector: 'app-source-info[Source]',
  templateUrl: './source-info.component.html',
  styleUrls: ['./source-info.component.css']
})
export class SourceInfoComponent implements OnInit {
  @Input() Source!: Source;

  faRotate=faRotate;
  Syncing: boolean = false;
  

  constructor(private sourceService: SourceService) {  }

  ngOnInit(): void { }

  syncNow() : void {
    this.Syncing = true;
    this.sourceService.sync().subscribe(() => {
      this.Syncing = false;
    });
  }
}