import { Component, OnInit } from '@angular/core';
import { SourceService } from 'src/app/services/source/source.service';
import { CDocCookieService } from 'src/app/services/config/cdoc.cookie.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-default',
  templateUrl: './default.component.html',
  styleUrls: ['./default.component.css']
})
export class DefaultComponent implements OnInit {
  constructor(
    private sourceService: SourceService, 
    private cookieService: CDocCookieService,
    private router:Router
  ) { }

  ngOnInit(): void { 
    let repository = this.cookieService.get("CDoc.CurrentRepository");
    if(repository != null && repository != '') {
      this.router.navigate(['view', repository]);
    }
    
    this.sourceService.getAll().subscribe(sources => {
      this.router.navigate(['view', sources[0].repository]);
    });     
  }
}
