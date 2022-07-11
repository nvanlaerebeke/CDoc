using Markdown.Process.Objects;

namespace Markdown.Process;

public interface IDocumentProvider
{
    IEnumerable<Item> Ls(string path);
    DocumentInfo GetDocumentInfo(string path);
}