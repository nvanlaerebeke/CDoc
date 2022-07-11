using CDoc.Error;

namespace CDoc.Process;

internal class SourceActions : ISourceActions
{
    public void SyncAll()
    {
        foreach (var source in new SourceProvider().GeAll())
        {
            source.Sync();   
        }
    }

    public void Sync(string repository)
    {
        var source = new SourceProvider().Get(repository);
        if (source == null)
        {
            throw new CDocException(new ApiError(ApiErrorCode.RepositoryNotFound));
        }
        source.Sync();
    }
}