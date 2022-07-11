namespace CDoc.Process;

public interface ISourceActions
{
    void SyncAll();
    void Sync(string repository);
}