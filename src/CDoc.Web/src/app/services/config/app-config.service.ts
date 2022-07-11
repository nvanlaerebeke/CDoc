import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AppConfigService {
  private _appConfig: any;
  private _http : HttpClient;
  
  constructor(http: HttpClient) {
	  this._http = http;
  }

  async loadAppConfig() {
    this._appConfig = await lastValueFrom(this._http.get('/assets/cdoc.web.json'));
  }

  get apiUrl(): string {    
    return this.indexTrim(this._appConfig.apiBaseUrl, '/');
  }

  private indexTrim(str: string, ch: string) {
    var start = 0, end = str.length;
    while(start < end && str[start] === ch) {
      ++start;
    }
    while(end > start && str[end - 1] === ch) {
      --end;
    }
    return (start > 0 || end < str.length) ? str.substring(start, end) : str;
  }
}