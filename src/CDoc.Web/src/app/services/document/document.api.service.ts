import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { AppConfigService } from '../config/app-config.service';
import { ApiItem } from 'src/app/types/Api/ApiItem';
import { Preview } from 'src/app/types/Preview';
import { ApiListResponse } from 'src/app/types/ApiListResponse';
import { ApiResponse } from 'src/app/types/ApiResponse';

import { Item } from 'src/app/types/Item';
import { HtmlConverter } from '../preview/HtmlConverter';

@Injectable({
  providedIn: 'root'
})

export class DocumentApiService {
  private _apiUrl: string;
  
  constructor(private http: HttpClient, private appConfigService: AppConfigService) {
    this._apiUrl = this.appConfigService.apiUrl + "/Document/";
  }
  
  get(repository: string, path: string): Observable<Item> {
    return this.http.get<ApiResponse<ApiItem>>(this._apiUrl + encodeURIComponent(repository) + "/" + encodeURIComponent(path)).pipe(
      map(response => {
        return new Item(repository, response.data);
      }
    ));
  }

  list(repository: string, path: string): Observable<Item[]> {
    return this.http.get<ApiListResponse<ApiItem>>(this._apiUrl + "List/" + encodeURIComponent(repository) + "/" + encodeURIComponent(path)).pipe(
      map(response => { 
        let items: Item[] = [];
        response.data.forEach(apiItem => {
          items.push(new Item(repository, apiItem));
        });
        return  items;
      })
    );
  }

  getPreview(repository: string, path: string): Observable<Preview> {
    return this.http.get<ApiResponse<Preview>>(this._apiUrl + "Preview/" + encodeURIComponent(repository) + "/" + encodeURIComponent(path)).pipe(
      map(response => {
        response.data.html = new HtmlConverter(this._apiUrl).Convert(repository, response.data.html);
        return response.data;
      }
    ));
  }
  
  getDataUrl(repository: string, path: string) : URL {
    return new URL(this._apiUrl + "GetRawData/" + encodeURIComponent(repository) + "/" + encodeURIComponent(path));
  }
}
