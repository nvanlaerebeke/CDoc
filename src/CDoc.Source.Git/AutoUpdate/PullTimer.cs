using CDoc.Source.Git.Actions;

namespace CDoc.Source.Git.AutoUpdate;

internal class PullTimer
{
    private readonly ExternalGitActions _gitActions;
    private readonly PeriodicTimer _timer;

    public PullTimer(ExternalGitActions gitActions, int seconds)
    {
        _gitActions = gitActions;
        _timer = new PeriodicTimer(TimeSpan.FromSeconds(seconds));
    }
    public async Task Start()
    {
        while (await _timer.WaitForNextTickAsync())
        {
            _gitActions.Pull();
        }
    }
}