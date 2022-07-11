namespace Markdown.Process;

public class ActionProvider
{
    public IDocumentProvider GetDataProvider()
    {
        return new DocumentProvider();
    } 
}