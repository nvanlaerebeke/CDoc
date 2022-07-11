import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiSource } from 'src/app/types/Api/ApiSource'
import { Source } from 'src/app/types/Source'
import { ApiListResponse } from 'src/app/types/ApiListResponse'
import { AppConfigService } from '../config/app-config.service';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class SourceService {
  private _apiUrl: string;

  constructor(private http: HttpClient, private appConfigService: AppConfigService) { 
    this._apiUrl = this.appConfigService.apiUrl + "/Source/";
  }

  sync() {
    return this.http.get(this._apiUrl + "Sync");
  }

  getAll() : Observable<Source[]> {
    return this.http.get<ApiListResponse<ApiSource>>(this._apiUrl).pipe(
      map(response => { 
        let sources: Source[] = [];
        response.data.forEach(apiSource => {
          sources.push(new Source(apiSource));
        });
        return  sources;
      })
    );
  }
}
