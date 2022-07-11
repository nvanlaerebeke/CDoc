import { Component, OnInit } from '@angular/core';
import { CDocCookieService } from 'src/app/services/config/cdoc.cookie.service';

@Component({
  selector: 'app-cookie-alert',
  templateUrl: './cookie-alert.component.html',
  styleUrls: ['./cookie-alert.component.css']
})
export class CookieAlertComponent implements OnInit {
  CookiesAllowed: boolean = true;
  CookiesRefused: boolean = false;

  constructor(private cDocCookieService: CDocCookieService) { }

  ngOnInit(): void {
    if(!this.cDocCookieService.get("CDoc.AllowCookies")) {
      this.CookiesAllowed = false;
    } else {
      this.CookiesAllowed = true;
    }
  }

  acceptCookies() {
    this.CookiesAllowed = true;
    this.cDocCookieService.setCookiesAllowed();
  }

  refuseCookies() {
    this.CookiesRefused = true;
  }
}
