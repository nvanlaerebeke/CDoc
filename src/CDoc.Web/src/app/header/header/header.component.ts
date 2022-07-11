import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { Page } from "../../types/Enum/Page";
import { faGears, faBook } from '@fortawesome/free-solid-svg-icons';
import { CDocCookieService } from 'src/app/services/config/cdoc.cookie.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  faGears = faGears;
  faBook = faBook;

  Pages = Page;
  Display: string;
  Repository: string;
  Page!: Page;

  constructor(
    private cookieService: CDocCookieService, 
    private router:Router
  ) { 
    this.Display = this.cookieService.get('CDoc.Display') ?? "markdown";
    this.Repository = this.cookieService.get('CDoc.CurrentRepository') ?? "";
  }

  ngOnInit(): void { 
    this.cookieService.watch("CDoc.Display").subscribe(d => {
      this.Display = d;
    });

    this.cookieService.watch("CDoc.CurrentRepository").subscribe(r => {
      this.Repository = r;
    });

    this.router.events.subscribe(e => {
      if(e instanceof NavigationEnd) {
        this.Page = (e.url == '/settings') ? Page.Settings : Page.Viewer;
        if(this.Page == Page.Viewer) {
          this.cookieService.set("CDoc.LastViewedDocument", e.url);
        }
      }
    });
  }
  
  onDisplayChanged(display: string) {
    this.cookieService.set("CDoc.Display", display);
  }

  onRepositoryChanged(repository: string) {
    this.cookieService.unset("CDoc.LastViewedDocument");
    this.cookieService.set("CDoc.CurrentRepository", repository);
    this.router.navigate(['view']);
  }

  goToView() :void {
    let lastViewedDocument = this.cookieService.get("CDoc.LastViewedDocument");
    if(lastViewedDocument) {
      this.router.navigateByUrl(lastViewedDocument);
    } else {
      this.router.navigate(['view']);
    }
  }

  goToSettings() : void {
    this.router.navigate(['settings']);
  }
}
