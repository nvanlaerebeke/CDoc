namespace CDoc.Source;

public class Item
{
    public string Name { get; }
    public string Path { get; }
    public int Size { get; }
    public string Type { get; }

    public Item(string name, string path, int size, string type)
    {
        Name = name;
        Path = path;
        Size = size;
        Type = type;
    }
}