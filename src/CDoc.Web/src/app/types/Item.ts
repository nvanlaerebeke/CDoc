import { ApiItem } from 'src/app/types/Api/ApiItem';
import { MimeType } from 'src/app/types/MimeType';

export class Item {
    repository: string;
    name: string;
    path: string;
    size: number;
    sizeHumanReadable: string;
    mimetype: string;
    hasChildren?: boolean;
    hasPreview: boolean;
    previewType: string;

    isOpen: boolean = false;
    children: Item[] = new Array<Item>;
    
    constructor(repository: string, apiItem: ApiItem) {
        this.repository = decodeURIComponent(repository);
        this.name = apiItem.name;
        this.path = apiItem.path;
        this.size = apiItem.size;
        this.mimetype = apiItem.mimetype;
        this.hasPreview = apiItem.hasPreview;
        this.sizeHumanReadable = this.formatBytes(this.size, 2);
        this.previewType = MimeType.getPreviewer(apiItem.mimetype);
    }

    private formatBytes(bytes: number, decimals: number) {
        if(bytes == 0) return '0 Bytes';
        var k = 1024,
            dm = decimals || 2,
            sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'],
            i = Math.floor(Math.log(bytes) / Math.log(k));
        return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ' ' + sizes[i];
     }
}