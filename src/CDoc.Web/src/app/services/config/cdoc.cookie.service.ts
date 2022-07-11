import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CDocCookieService {
  private _allowCookies = false;
  private _allowCookieName = "CDoc.AllowCookies";

  private _sessionCache: Map<string, string> = new Map<string, string>();
  private _watchers: Map<string, Subject<string>> = new Map<string, Subject<string>>();

  constructor(private cookieService:CookieService) { 
    if(this.cookieService.get(this._allowCookieName)) {
      this._allowCookies = true;
    }
  }

  setCookiesAllowed() {
    this.cookieService.delete(this._allowCookieName);
    this.cookieService.set(this._allowCookieName, "CookiesAllowed");
    this._allowCookies = true;
  }

  set(key: string, value: string): void {
    //value did not change
    if(this._sessionCache.has(key) && this._sessionCache.get(key) == value) {
      return;
    }

    //do nothing, storing cookies is not allowed
    if(!this._allowCookies) {
      this._sessionCache.set(key, value);
      this.notifyWatchers(key, value);
      return;
    }

    this.cookieService.delete(key);
    this.cookieService.set(key, value);

    this.notifyWatchers(key, value);
  }

  get(key: string): string | undefined {
    if(this._sessionCache.has(key)) {
      return this._sessionCache.get(key);
    }
    //only read cookie if cookie consent has been given
    return this._allowCookies ? this.cookieService.get(key) : undefined;
  }

  unset(key: string) {
    this._sessionCache.delete(key);
    this.cookieService.delete(key);
    this.notifyWatchers(key, "");
  }

  watch(key: string) : Subject<string> {
    if(this._watchers.has(key)) {
      return this._watchers.get(key)!;
    }
    var subject = new Subject<string>();
    this._watchers.set(key, subject);
    return subject;
  }

  private notifyWatchers(key: string, value: string) {
    if(this._watchers.has(key)) {
      this._watchers.get(key)?.next(value);
    }
  }
}
