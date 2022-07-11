import { IconProp } from '@fortawesome/fontawesome-svg-core';

import { faMarkdown } from '@fortawesome/free-brands-svg-icons';
import { faFilePdf } from '@fortawesome/free-solid-svg-icons';
import { faFolder, faFile, faImage } from  "@fortawesome/free-solid-svg-icons"

//import {  } from  "@fortawesome/angular-fontawesome"
//import {  } from  "@fortawesome/fontawesome-common-types"
//import {  } from  "@fortawesome/fontawesome-svg-core"
//import {  } from  "@fortawesome/free-brands-svg-icons"
//import {  } from  "@fortawesome/free-regular-svg-icons"

export class MimeType {

    static getIcon(mimeType: string) : IconProp{
        switch(mimeType) {
            case 'text/markdown':
                return faMarkdown;
            case 'image/png':
            case 'image/jpg':
            case 'image/jpeg':
                return faImage;
            case "application/pdf":
                return faFilePdf;
            case 'Directory':
                return faFolder;
            default:
                return faFile;
        }
    }

    static getPreviewer(mimeType: string): string {
        switch(mimeType) {
            case 'text/markdown':
                return 'html';
            case 'image/png':
            case 'image/jpg':
            case 'image/jpeg':
                return "image";
            case "application/pdf":
                return "pdf";
            default:
                return "file";
        }
    }

    static isDocument(mimeType: string): boolean {
        switch(mimeType) {
            case 'text/markdown':
            case "application/pdf":
                return true;
            default:
                return false;
        }
    }
}