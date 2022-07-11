namespace CDoc.Process;

public class ActionProvider : IActionProvider
{
    public IDocumentProvider GetDocumentProvider()
    {
        return new DocumentProvider(new SourceProvider());
    }

    public ISourceActions GetSourceActions()
    {
        return new SourceActions();
    }
}