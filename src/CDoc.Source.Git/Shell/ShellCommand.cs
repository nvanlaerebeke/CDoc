using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace CDoc.Source.Git.Shell;

[ExcludeFromCodeCoverage]
internal class ShellCommand
{
    private readonly string _script;
    private readonly Dictionary<string, string> _environmentVariables = new();
    public ShellCommand(string script)
    {
        _script = script;
    }

    public void SetEnvironmentVariable(string key, string value)
    {
        _environmentVariables[key] = value;
    }

    public string Arguments { get; set; } = "";
    public string WorkingDirectory { get; set; } = "/";

    public bool Run()
    {
        using (var myProcess = new Process())
        {
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.CreateNoWindow = false;
            myProcess.StartInfo.FileName = _script;
            foreach (var kvp in _environmentVariables)
            {
                myProcess.StartInfo.EnvironmentVariables[kvp.Key] = kvp.Value;
            }

            if (!string.IsNullOrEmpty(WorkingDirectory))
            {
                myProcess.StartInfo.WorkingDirectory = WorkingDirectory;
            }

            if (!string.IsNullOrEmpty(Arguments))
            {
                myProcess.StartInfo.Arguments = Arguments;
            }

            _ = myProcess.Start();

            myProcess.WaitForExit();
            return myProcess.ExitCode == 0;
        }
    }
}