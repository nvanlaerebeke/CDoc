using System.Collections.Concurrent;

namespace CDoc.HtmlProvider;

public class HtmlProviderFactory
{
    private static readonly List<IHtmlProvider> _htmlProviders = new();
    private static readonly ConcurrentDictionary<string, IHtmlProvider> _quickCache = new();
    public static IHtmlProvider? Get(string mimeType)
    {
        if (_quickCache.ContainsKey(mimeType))
        {
            if (_quickCache.TryGetValue(mimeType, out var provider))
            {
                return provider;
            }
        }
        
        foreach (var htmlProvider in _htmlProviders.Where(p => p.SupportedMimeTypes().Contains(mimeType)))
        {
            if (!_quickCache.ContainsKey(mimeType))
            {
                _quickCache.TryAdd(mimeType, htmlProvider);
            }
            return htmlProvider;
        }
        
        return null;
    }

    public static void Add(IHtmlProvider htmlProvider)
    {
        _htmlProviders.Add(htmlProvider);
    }
}