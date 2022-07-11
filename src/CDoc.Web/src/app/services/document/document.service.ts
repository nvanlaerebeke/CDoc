import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

import { Item } from 'src/app/types/Item';
import { Preview } from 'src/app/types/Preview';
import { RepositoryDocumentCache } from '../cache/repository.document.cache';
import { DocumentApiService } from './document.api.service';

@Injectable({
  providedIn: 'root'
})
export class DocumentService { 

  constructor(
    private documentCache: RepositoryDocumentCache,
    private documentApiService: DocumentApiService
  ) {  

  }

  get(repository: string, path: string): Observable<Item> {
    return this.documentApiService.get(repository, path);
  }

  list(repository: string, path: string): Observable<Item[]> {
    var cachedResponse = this.documentCache.getItem<Item[]>(repository, "list", path);
    if(!cachedResponse) {
      cachedResponse = this.documentApiService.list(repository, path);
      this.documentCache.setItem(repository, "list", path, cachedResponse);
    } 
    return cachedResponse;
  }

  getPreview(repository: string, path: string): Observable<Preview> {
    var cachedResponse = this.documentCache.getItem<Preview>(repository, "getPreview", path);
    if(!cachedResponse) {
      cachedResponse = this.documentApiService.getPreview(repository, path);
      this.documentCache.setItem(repository, "getPreview", path, cachedResponse);
    } 
    return cachedResponse;
  }

  getDefaultPath(repository: string) : Observable<Item | null> {
    var subject = new Subject<Item | null>();

    this.list(repository, "/").subscribe(items => {       
      let item = this.getDefaultItem(items);
      subject.next(item);
    });
    
    return subject;
  }

  getDefaultItem(items: Item[]) : Item | null{
      //Select the default page, priority is:
      //1. index
      //2. home
      //3. any other markdown document that is found first
      let selectedItem: Item | null = null;
      
      items.forEach(i => {       
        if(i.name.toLowerCase() == "index") {
          selectedItem = i; 
          return false; //quit right away, index has highest priority
        }

        if(
          i.name.toLowerCase() == "home" && 
          selectedItem?.name.toLowerCase() != "index"
        ) {
            selectedItem = i;
        }

        if(selectedItem == null && i.mimetype == "text/markdown") {
          selectedItem = i;
        }
        return true;
      });
      return selectedItem;
  }
  
  getDataUrl(repository: string, path: string) {
    return this.documentApiService.getDataUrl(repository, path);
  }
}