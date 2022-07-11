using HtmlAgilityPack;

namespace CDoc.HtmlProvider.Markdown;

/// <summary>
/// This class is to modify the DOM so it can be used to display on external sources.
//
// Modifications include:
//
// - Change img tags so that they refer to a downloadable url
//
/// </summary>
internal class HtmlModifier
{

    public string ApplyCDocChanges(string html, string documentPath)
    {
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        var nodes = htmlDoc.DocumentNode.SelectNodes("//img");
        if (nodes == null)
        {
            return html;
        }
        
        foreach(var node in nodes)
        {
            if (!node.Attributes.Contains("src"))
            {
                continue;
            }
            //If there is a URL filled in stead of a relative path, use that
            if (Uri.TryCreate(node.Attributes["src"].Value, UriKind.Absolute, out var url))
            {
                continue;
            }
            node.Attributes.Add("data-src", GetPath(documentPath, node.Attributes["src"].Value));
        }
        
        using var writer = new StringWriter();
        htmlDoc.Save(writer);
        return writer.ToString();
    }

    private static string GetPath(string documentPath, string targetPath)
    {
        return Path.GetFullPath(
            Path.Combine(
                Path.GetDirectoryName(documentPath)!, 
                targetPath.Trim('/')
            )
        );
    }
}