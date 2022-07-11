namespace CDoc.Source;

public interface ISource
{
    string RootPath { get; }
    IEnumerable<Item> Ls(string path);
    Item? Get(string path);
    string GetContent(string path);
    void Sync();
    FileInfo GetFileInfo(string path);
}