using CDoc.Process.Objects;

namespace CDoc.Process;

internal class DocumentProvider : IDocumentProvider
{
    public IEnumerable<Item> Ls(string path)
    {
        return MockData.Ls(path);
    }

    public DocumentInfo GetDocumentInfo(string path)
    {
        return null;
    }
}