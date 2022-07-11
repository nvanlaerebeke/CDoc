using System.Text;
using CDoc.Source.Git.Actions;
using Mono.Unix.Native;

namespace CDoc.Source.Git;

public class GitSshCredentials
{
    private readonly string _repository;
    private readonly string _privateKey;
    
    private string TempPath => "/tmp/" + _repository.Split("/").Last();
    public string ScriptPath => Path.Combine(TempPath, "git_ssh.sh");
    private string KeyPath => Path.Combine(TempPath, "git.key");
    private string GitScript => $"#!/bin/sh\nexec /usr/bin/ssh -o StrictHostKeyChecking=no -i {KeyPath} \"$@\"";
    
    public GitSshCredentials(string repository, string privateKey)
    {
        _repository = repository;
        _privateKey = privateKey;
    }

    public void Setup()
    {
        if (!Directory.Exists(TempPath))
        {
            Directory.CreateDirectory(TempPath);
        }
        
        File.WriteAllText(ScriptPath, GitScript);
        File.WriteAllText(KeyPath, Encoding.UTF8.GetString(Convert.FromBase64String(_privateKey)));

        Syscall.chmod(ScriptPath,
            FilePermissions.S_IRGRP |
            FilePermissions.S_IRUSR |
            FilePermissions.S_IROTH |
            FilePermissions.S_IXUSR |
            FilePermissions.S_IXGRP |
            FilePermissions.S_IXOTH
        );
        Syscall.chmod(KeyPath, FilePermissions.S_IRUSR);
    }
}