using Markdig;

namespace CDoc.HtmlProvider.Markdown;

public class MarkdownHtmlProvider : IHtmlProvider
{
    private MarkdownPipeline? _markdownPipeline;
   
    private MarkdownPipeline GetPipeline()
    {
        if (_markdownPipeline != null)
        {
            return _markdownPipeline;
        }

        _markdownPipeline = new MarkdownPipelineBuilder()
            .UseAbbreviations()
            .UseAutoIdentifiers()
            .UseBootstrap()
            .UseCitations()
            .UseCustomContainers()
            .UseDefinitionLists()
            .UseDiagrams()
            .UseEmojiAndSmiley()
            .UseEmphasisExtras()
            .UseFigures()
            .UseFooters()
            .UseFootnotes()
            .UseSoftlineBreakAsHardlineBreak()
            .UseListExtras()
            .UseGridTables()
            .UseMathematics()
            .UseMediaLinks()
            .UsePragmaLines()
            .UsePipeTables()
            .UseSmartyPants()
            .UseNonAsciiNoEscape()
            .UseListExtras()
            .UseTaskLists()
            .UseYamlFrontMatter()
            .UseReferralLinks()
            .UseDiagrams()
            .UseAutoLinks()
            .UseGenericAttributes() // Must be last as it is one parser that is modifying other parsers
            .Build();

        return _markdownPipeline;
    }

    public IEnumerable<string> SupportedMimeTypes()
    {
        return new List<string> {"text/markdown"};
    }

    public string Get(FileInfo fileInfo, string repositoryRoot)
    {
        return new HtmlModifier().ApplyCDocChanges(Markdig.Markdown.ToHtml(File.ReadAllText(fileInfo.FullName), GetPipeline()), fileInfo.FullName[repositoryRoot.Length..]);
    }
}