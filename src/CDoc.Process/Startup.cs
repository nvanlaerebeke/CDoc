using CDoc.HtmlProvider;
using CDoc.HtmlProvider.Markdown;

namespace CDoc.Process;

public static class Startup
{
    public static void Start()
    {
        new HtmlProviderFactory().UseMarkdown();
        
        SourceProvider.Initialize();
    }
}