export class HtmlConverter {
  private _apiUrl: string;

    constructor(apiUrl: string) {
        this._apiUrl = apiUrl;
    }

    public Convert(repository:string, html: string): string {
      var preview = document.createElement("div");
      preview.innerHTML = html;

      var images = preview.getElementsByTagName("img");
      for(let i = 0; i < images.length; i++) {
        if(images[i].hasAttribute('data-src')) {
            images[i].setAttribute('src', this.GetDownloadUrl(repository, images[i].getAttribute("data-src")!));
        }
      }
      return preview.innerHTML;
    }

    private GetDownloadUrl(repository: string, path: string): string {
      return this._apiUrl + "GetRawData/" + encodeURIComponent(repository) + "/" + encodeURIComponent(path);
    }
}