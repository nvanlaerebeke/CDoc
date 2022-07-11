namespace CDoc.Source.Git;

public interface IGitSourceFactory
{
    ISource Create(string repository, GitSshCredentials credentials, int autoRefreshSeconds, bool pullBeforeRead);
}