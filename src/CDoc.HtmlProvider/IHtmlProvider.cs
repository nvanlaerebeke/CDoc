namespace CDoc.HtmlProvider;

public interface IHtmlProvider
{
    IEnumerable<string> SupportedMimeTypes();
    string Get(FileInfo fileInfo, string repositoryRoot);
}