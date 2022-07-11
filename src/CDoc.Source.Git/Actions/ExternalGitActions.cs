using CDoc.Source.Git.Shell;

namespace CDoc.Source.Git.Actions;

internal class ExternalGitActions
{
    private static readonly object _lock = new();

    private readonly string _fullPath;
    private readonly string _repository;    
    public readonly Dictionary<string, string> EnvironmentVariables = new();

    public ExternalGitActions(string repository, string fullPath)
    {
        _repository = repository;
        _fullPath = fullPath;
    }
    
    public void Clone()
    {
        var path = Path.GetDirectoryName(_fullPath); 
        RunCommand($"clone \"{_repository}\" \"{_fullPath}\"", path!);
    }

    public void Pull()
    {
        RunCommand("pull");
    }

    public void Push()
    {
        RunCommand("push");
    }

    private void RunCommand(string arguments, string workingDirectory = "")
    {
        lock (_lock)
        {
            var cmd = new ShellCommand("git");
            foreach (var envVar in EnvironmentVariables)
            {
                cmd.SetEnvironmentVariable(envVar.Key, envVar.Value);
            }

            cmd.WorkingDirectory = string.IsNullOrEmpty(workingDirectory) ? _fullPath : workingDirectory;
            cmd.Arguments = arguments;
            cmd.Run();
        }
    }
}