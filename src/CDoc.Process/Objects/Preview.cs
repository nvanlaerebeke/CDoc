namespace CDoc.Process.Objects;

public class Preview
{
    public Preview(string name, string path, string html)
    {
        Name = name;
        Path = path;
        Html = html;
    }
    
    public string Name { get; }
    public string Path { get; }
    public string Html { get; }
}