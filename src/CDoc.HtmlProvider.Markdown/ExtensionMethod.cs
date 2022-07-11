namespace CDoc.HtmlProvider.Markdown;

public static class ExtensionMethod
{
    public static void UseMarkdown(this HtmlProviderFactory _)
    {
        HtmlProviderFactory.Add(new MarkdownHtmlProvider());
    }
}