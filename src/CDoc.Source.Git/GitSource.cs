using CDoc.Source.Git.Actions;
using CDoc.Source.Git.AutoUpdate;

namespace CDoc.Source.Git;

internal class GitSource : ISource
{
    private readonly string _repository;
    private readonly GitSshCredentials _credentials;
    private readonly int _autoUpdateSeconds;
    private readonly bool _pullBeforeRead;

    private ExternalGitActions? _externalGitActions;
    private GitActions? _gitActions;
    private LocalActions? _localActions;
    private PullTimer? _autoUpdate;

    private bool _initialized;
    
    public GitSource(string repository, GitSshCredentials credentials, string tempPath, int autoUpdateSeconds, bool pullBeforeRead)
    {
        _repository = repository;
        _credentials = credentials;
        _autoUpdateSeconds = autoUpdateSeconds;
        _pullBeforeRead = pullBeforeRead;

        RootPath = Path.Combine(tempPath, repository.Split('/').Last());
    }

    public void Initialize()
    {
        if (!Directory.Exists(Path.GetDirectoryName(RootPath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(RootPath)!);
        }

        _externalGitActions = new(_repository, RootPath);
        _localActions = new(RootPath);
        
        _credentials.Setup();
        _externalGitActions.EnvironmentVariables.Add("GIT_SSH", _credentials.ScriptPath);
        
        if (!Directory.Exists(RootPath))
        {
            _externalGitActions.Clone();
            _gitActions = new(RootPath);
        }
        else
        {
            _gitActions = new(RootPath);
            _gitActions.Reset();
            _externalGitActions.Pull();            
        }

        if (_autoUpdateSeconds > 0)
        {
            _autoUpdate = new(_externalGitActions, _autoUpdateSeconds);
            _autoUpdate.Start().ConfigureAwait(false);
        }

        _initialized = true;
    }

    public string RootPath { get; }
    
    public IEnumerable<Item> Ls(string path)
    {
        if (!_initialized)
        {
            throw new TypeInitializationException(GetType().ToString(), null);
        }
        if (_pullBeforeRead)
        {
            _externalGitActions!.Pull();
        }
        return _localActions!.Ls(path);
    }

    public FileInfo GetFileInfo(string path)
    {
        if (!_initialized)
        {
            throw new TypeInitializationException(GetType().ToString(), null);
        }
        if (_pullBeforeRead)
        {
            _externalGitActions!.Pull();
        }
        return _localActions!.GetFileInfo(path);
    }

    public string GetContent(string path)
    {
        if (!_initialized)
        {
            throw new TypeInitializationException(GetType().ToString(), null);
        }
        if (_pullBeforeRead)
        {
            _externalGitActions!.Pull();
        }
        return _localActions!.GetContents(path);
    }

    public void Sync()
    {
        if (!_initialized)
        {
            throw new TypeInitializationException(GetType().ToString(), null);
        }
        _gitActions!.Reset();
        _externalGitActions!.Pull(); 
    }

    public Item? Get(string path)
    {
        if (!_initialized)
        {
            throw new TypeInitializationException(GetType().ToString(), null);
        }
        if (_pullBeforeRead)
        {
            _externalGitActions!.Pull();
        }
        return _localActions!.Get(path);
    }
}