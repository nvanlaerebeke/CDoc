import { Component, OnInit, Input } from '@angular/core';
import { Source } from 'src/app/types/Source';

@Component({
  selector: 'app-source[Source]',
  templateUrl: './source.component.html',
  styleUrls: ['./source.component.css']
})
export class SourceComponent implements OnInit {
  @Input() Source!: Source;

  constructor() { }

  ngOnInit(): void {
  }
}
