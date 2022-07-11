import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { DocumentCache } from "./api.cache";

@Injectable({
    providedIn: 'root'
})
export class RepositoryDocumentCache {
    private _repositoryCaches = new Map<string, DocumentCache>();

    getItem<T>(repository: string, method: string, key: string): Observable<T> | undefined {
        return this.getCacheForRepository(repository).getItem(method, key);
    }
    
    setItem<T>(repository: string, method: string, key: string, value: Observable<T>) {
        return this.getCacheForRepository(repository).setItem(method, key, value);
    }

    private getCacheForRepository(repository: string) : DocumentCache {
        if(!this._repositoryCaches.has(repository)) {
            this._repositoryCaches.set(repository, new DocumentCache());    
        }
        return this._repositoryCaches.get(repository)!;
    }
}