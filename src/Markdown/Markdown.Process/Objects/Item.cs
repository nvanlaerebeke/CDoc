namespace Markdown.Process.Objects;

public class Item
{
    public Item(string name, string path, string type, bool hasChildren)
    {
        Name = name;
        Path = path;
        Type = type;
        HasChildren = hasChildren;
    }
    
    public string Name { get; }
    public string Path { get; }
    public string Type { get; }
    public bool HasChildren { get; }
}