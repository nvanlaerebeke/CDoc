import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { DocumentService } from 'src/app/services/document/document.service';
import { Item } from 'src/app/types/Item';
import { CDocCookieService } from 'src/app/services/config/cdoc.cookie.service';
import { NavigationInfo } from 'src/app/types/NavigationInfo';
@Injectable({
  providedIn: 'root'
})
export class ItemResolver implements Resolve<NavigationInfo> {
  constructor(private documentService: DocumentService, private cookieService: CDocCookieService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<NavigationInfo> {
    let subject = new Subject<NavigationInfo>();

    let repositorySegment = route.url.at(0);
    let pathSegment = route.url.at(1);
    
    //ToDo: figure out where the double encode is originating from
    let repository = decodeURIComponent(repositorySegment!.path);
    //in case the url being parsed is different than the current selected repository
    this.cookieService.set('CDoc.CurrentRepository', repository);

    if(pathSegment == null) {
      this.documentService.getDefaultPath(repository).subscribe(item => {
        subject.next(new NavigationInfo(repository, item));
      })
    } else {
      this.documentService.get(repository, decodeURIComponent(pathSegment.path)).subscribe(i =>{
        subject.next(new NavigationInfo(repository, i));
      });
    } 
    return subject;    
  }
}
