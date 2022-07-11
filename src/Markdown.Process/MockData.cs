using Markdown.Process.Objects;

namespace Markdown.Process;

internal static class MockData
{
    private static Dictionary<string, IEnumerable<Item>> _dictionary = new()
    {
        { "/", new List<Item>()
        {
            new Item("Introduction", "/Introduction", "Directory", true),
            new Item ("Index", "/Index.md", "Markdown", false)
        }}
    };
    
    public static IEnumerable<Item> Ls(string path)
    {
        return _dictionary.ContainsKey(path) ? _dictionary[path] : null;
    }
}