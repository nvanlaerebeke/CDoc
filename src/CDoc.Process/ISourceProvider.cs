using CDoc.Source;

namespace CDoc.Process;

internal interface ISourceProvider
{
    ISource? Get(string repository);
}