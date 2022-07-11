import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { faChevronDown } from  "@fortawesome/free-solid-svg-icons"
import { SourceService } from 'src/app/services/source/source.service';
import { Source } from 'src/app/types/Source'
import { Router } from '@angular/router';

@Component({
  selector: 'app-source',
  templateUrl: './source.component.html',
  styleUrls: ['./source.component.css']
})
export class SourceComponent implements OnInit {
  faChevronDown = faChevronDown;
  Sources: Source[] = [];

  @Input() Selected!: string;
  @Output() repositoryChanged = new EventEmitter<string>();

  constructor(
    private router:Router,
    private sourceService: SourceService
  ) { }

  ngOnInit(): void {
    this.sourceService.getAll().subscribe(sources => {
      this.Sources = sources;
    });
  }

  selectOption(selected: string) {
    this.router.navigate(['view', selected]);
  }
}
