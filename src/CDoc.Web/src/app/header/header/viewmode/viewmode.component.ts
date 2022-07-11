import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { faChevronDown } from  "@fortawesome/free-solid-svg-icons"

@Component({
  selector: 'app-viewmode[Selected]',
  templateUrl: './viewmode.component.html',
  styleUrls: ['./viewmode.component.css']
})

export class ViewmodeComponent implements OnInit {
  faChevronDown = faChevronDown;

  Display: string = "Markdown Only";

  @Input() Selected!: string;
  @Output() displayChanged = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
    this.setDisplay(this.Selected);
  }

  selectOption(selected: string) {
    this.setDisplay(selected);
    this.displayChanged.emit(this.Selected);
  }

  private setDisplay(display: string) {
    this.Selected = display;
    switch(this.Selected) {
      case "markdown":
        this.Display = "Markdown Only";
        break;
      case "documents":
        this.Display = "Documents Only";
        break;
      case "all":
        this.Display = "All";
        break;
    }
  }
}
