using CDoc.Config;
using CDoc.Source;
using CDoc.Source.Git;

namespace CDoc.Process;

internal class SourceProvider : ISourceProvider
{
    private static readonly Dictionary<string, ISource> _cache = new();
    
    public static void Initialize()
    {
        var config = new Settings().GetConfig();
        var gitSourceFactory = new GitSourceFactory("/cache/checkouts");
        
        if (config.Sources == null)
        {
            return;
        }

        foreach (var source in config.Sources)
        {
            if (!_cache.ContainsKey(source.Repository))
            {
                _cache.Add(
                    source.Repository, 
                    gitSourceFactory.Create(
                        source.Repository, 
                        new GitSshCredentials(source.Repository, source.SshKey),
                        source.AutoUpdateSeconds,
                        source.PullBeforeRead
                    )
                );
            }
        }
    }

    public ISource? Get(string repository)
    {
        return _cache.ContainsKey(repository) ? _cache.First(x => x.Key == repository).Value : null;
    }

    internal IEnumerable<ISource> GeAll()
    {
        return _cache.Values;
    }
}