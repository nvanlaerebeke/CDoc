import { Component, OnInit } from '@angular/core';
import { SourceService } from 'src/app/services/source/source.service';
import { Source } from 'src/app/types/Source';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {
  Sources: Source[] = [];

  constructor(private sourceService: SourceService) {  }

  ngOnInit(): void { 
    this.sourceService.getAll().subscribe(sources => {
      this.Sources = sources;
    });  
  }
}
