import { Observable } from "rxjs";

/** 
 * ToDo: Make the Observable<any> type safe
 *       This will to split this single Cache class into multiple classes
 *       
 *       Example:
 *          private _previewCache: PreviewCache = CacheFactory.Get(cacheType: CacheType);
 * 
 *       Where the factory will provide a type safe cache class based on a passed enum
 *           
**/
class Cache {
    private _cache = new Map<string, Map<string, Observable<any>>>();

    getItem<T>(method: string, key: string): Observable<T> | undefined {
        if(this._cache.has(method) && this._cache.get(method)?.has(key)) {
            return <Observable<T>>this._cache.get(method)?.get(key);
        }
        return;
    }
    
    setItem<T>(method: string, key: string, value: Observable<T>) {
        if(!this._cache.has(method)) {
            this._cache.set(method, new Map<string, Observable<T>>);
        }
        this._cache.get(method)?.set(key, value);
    }
}

export class DocumentCache extends Cache { }