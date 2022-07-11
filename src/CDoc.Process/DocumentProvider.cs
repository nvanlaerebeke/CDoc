using CDoc.Error;
using CDoc.HtmlProvider;
using CDoc.Process.Objects;
using CDoc.Source;
using Item = CDoc.Process.Objects.Item;

namespace CDoc.Process;

internal class DocumentProvider : IDocumentProvider
{
    private readonly SourceProvider _sourceProvider;
    private readonly string[] _ignores = {".git"};
    
    public DocumentProvider(SourceProvider sourceProvider)
    {
        _sourceProvider = sourceProvider;
    }

    public Item? Get(string repository, string path)
    {
        var source = _sourceProvider.Get(repository);
        var obj = source?.Get(path);
        if (obj == null)
        {
            return null;
        }

        var type = DocTypeDetector.Get(obj.Name);
        return new Item(
            NameFilter.Filter(obj.Name),
            obj.Path,
            obj.Size,
            obj.Type == "Directory" ? "Directory" : type,
            HtmlProviderFactory.Get(type) != null
        );
    }
    
    public IEnumerable<Item>? Ls(string repository, string path)
    {
        return _sourceProvider.Get(repository)?.Ls(path)
            .Where(i => !_ignores.Contains(i.Name))
            .OrderBy(i => i.Type != "Directory").ThenBy(i => i.Name)
            .Select(i =>
            {
                var type = DocTypeDetector.Get(i.Name);
                return new Item(
                    NameFilter.Filter(i.Name),
                    i.Path,
                    i.Size,
                    i.Type == "Directory" ? "Directory" : type,
                    HtmlProviderFactory.Get(type) != null
                );
            })
        ;
    }

    public Preview? Preview(string repository, string path)
    {
        var htmlProvider = HtmlProviderFactory.Get(DocTypeDetector.Get(Path.GetFileName(path)));
        if (htmlProvider == null)
        {
            throw new CDocException(new ApiError(ApiErrorCode.UnSupportedPreviewType));
        }
        
        var source = _sourceProvider.Get(repository);
        if (source == null)
        {
            return null;
        }
        
        return new Preview(
            NameFilter.Filter(Path.GetFileName(path)), 
            path, 
            htmlProvider.Get(source.GetFileInfo(path), source.RootPath)
        );
    }

    public FileInfo? GetFileInfo(string repository, string path)
    {
        return _sourceProvider.Get(repository)?.GetFileInfo(path);
    }
}