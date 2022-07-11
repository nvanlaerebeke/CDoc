using CDoc.Process.Objects;

namespace CDoc.Process;

public interface IDocumentProvider
{
    Item? Get(string repository, string path);
    IEnumerable<Item>? Ls(string repository, string path);
    Preview? Preview(string repository, string path);
    FileInfo? GetFileInfo(string repository, string path);
}