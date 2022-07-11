using System.Collections.Concurrent;

namespace CDoc.Source.Git;

public class GitSourceFactory : IGitSourceFactory
{
    private readonly string _tempPath;
    
    private static readonly ConcurrentDictionary<string, ISource> _sources = new();
    
    public GitSourceFactory(string tempPath)
    {
        _tempPath = tempPath;
    }
    
    public ISource Create(string repository, GitSshCredentials credentials, int autoUpdateSeconds, bool pullBeforeRead)
    {
        while (true)
        {
            if (_sources.ContainsKey(repository))
            {
                if (_sources.TryGetValue(repository, out var source))
                {
                    return source;
                }
            }

            var newSource = new GitSource(repository, credentials, _tempPath, autoUpdateSeconds, pullBeforeRead);
            newSource.Initialize();
            
            _sources.TryAdd(repository, newSource);
            return newSource;
        }
    }
}