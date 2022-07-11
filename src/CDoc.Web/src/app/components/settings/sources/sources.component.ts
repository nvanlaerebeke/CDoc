import { Component, OnInit, Input } from '@angular/core';
import { Source } from 'src/app/types/Source'

@Component({
  selector: 'app-sources[Sources]',
  templateUrl: './sources.component.html',
  styleUrls: ['./sources.component.css']
})
export class SourcesComponent implements OnInit {
  @Input() Sources!: Source[];

  constructor() { }

  ngOnInit(): void { }
}
