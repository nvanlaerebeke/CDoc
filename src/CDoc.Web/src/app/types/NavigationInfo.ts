import { Item } from 'src/app/types/Item'

export class NavigationInfo {
    Repository: string;
    Item: Item | null;

    constructor(repository: string, item: Item | null) {
        this.Repository = repository;
        this.Item = item;
    }
}