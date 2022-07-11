using System.Net;
using CDoc.Error;

namespace CDoc.Source.Git.Actions;

internal class LocalActions
{
    private readonly string _fullPath;

    public LocalActions(string fullPath)
    {
        _fullPath = fullPath;
    }

    public IEnumerable<Item> Ls(string path)
    {
        return
            new DirectoryInfo(Path.Combine(_fullPath, path.Trim('/')))
                .GetFileSystemInfos()
                .Select(fileSystemInfo =>
                    new Item(
                        fileSystemInfo.Name,
                        fileSystemInfo.FullName[_fullPath.Length..].Trim('/'),
                        (fileSystemInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory
                            ? 0
                            : (int) (fileSystemInfo as FileInfo)!.Length,
                        (fileSystemInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory ? "Directory" : "File"
                    )
                )
            ;
    }

    public FileInfo GetFileInfo(string path)
    {
        var fullPath = Path.Combine(_fullPath, path.Trim('/'));
        if (!File.Exists(fullPath))
        {
            throw new CDocException(new ApiError(ApiErrorCode.FileNotFound, HttpStatusCode.NotFound));
        }

        return new FileInfo(fullPath);
    }

    public string GetContents(string path)
    {
        return File.ReadAllText(Path.Combine(_fullPath, path.Trim('/')));
    }

    public Item? Get(string path)
    {
        var fullPath = Path.Combine(_fullPath, path.Trim('/'));
        if (!File.Exists(fullPath))
        {
            return null;
        }

        var fileInfo = new FileInfo(fullPath);
        return new Item(
            fileInfo.Name,
            fileInfo.FullName[_fullPath.Length..].Trim('/'),
            (fileInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory ? 0 : (int) fileInfo.Length,
            (fileInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory ? "Directory" : "File"
        );
    }
}