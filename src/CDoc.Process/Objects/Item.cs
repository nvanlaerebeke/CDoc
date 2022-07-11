namespace CDoc.Process.Objects;

public class Item
{
    public Item(string name, string path, int size, string mimetype, bool hasPreview)
    {
        Name = name;
        Path = path;
        Size = size;
        MimeType = mimetype;
        HasPreview = hasPreview;
    }

    public string Name { get; }
    public string Path { get; }
    public string MimeType { get; }
    public bool HasPreview { get; }
    public int Size { get; }
}