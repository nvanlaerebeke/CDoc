import { ApiSource } from 'src/app/types/Api/ApiSource';

export class Source {
    repository: string;
    name: string;
    cache: string;
    auto_update_seconds: number;
    pull_before_read: boolean;
    
    constructor(apiSource: ApiSource) {
        this.repository = apiSource.repository;
        this.cache = apiSource.cache;
        this.auto_update_seconds = apiSource.auto_update_seconds;
        this.pull_before_read = apiSource.pull_before_read;

        let repository_name = this.repository.split("/").pop();       
        if(repository_name != null) {
            this.name = repository_name.substring(0, repository_name.lastIndexOf('.')) || repository_name
        } else {
            this.name = this.repository;
        }
    }
}