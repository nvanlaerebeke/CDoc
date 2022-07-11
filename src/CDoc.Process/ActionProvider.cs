namespace CDoc.Process;

public class ActionProvider
{
    public IDocumentProvider GetDocumentProvider()
    {
        return new DocumentProvider();
    } 
}