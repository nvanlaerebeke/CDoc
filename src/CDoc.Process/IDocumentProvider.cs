using CDoc.Process.Objects;

namespace CDoc.Process;

public interface IDocumentProvider
{
    IEnumerable<Item> Ls(string path);
    DocumentInfo GetDocumentInfo(string path);
}