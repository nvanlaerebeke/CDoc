using Microsoft.AspNetCore.StaticFiles;

namespace CDoc.HtmlProvider;

public static class DocTypeDetector
{
    public static string Get(string fileName)
    {
        return new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var contentType) ? contentType : "application/binary";
    }
}