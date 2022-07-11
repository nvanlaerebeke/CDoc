using LibGit2Sharp;

namespace CDoc.Source.Git.Actions;

internal class GitActions
{
    private readonly Repository _repository;
    
    public GitActions(string repository)
    {
        _repository = new(repository);
    }
    public void Reset()
    {
        _repository.Reset(ResetMode.Hard);
    }
}