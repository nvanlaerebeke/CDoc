namespace CDoc.Error;

internal static class ApiErrorMessage
{
    private static readonly Dictionary<ApiErrorCode, string> _messages = new()
    {
        {ApiErrorCode.Critical, "A critical error occurred"},
        {ApiErrorCode.FileNotFound, "The requested file was not found"},
        {ApiErrorCode.InvalidValue, "Invalid parameter value"},
        {ApiErrorCode.Unknown, "An unknown error occurred"},
        {ApiErrorCode.RepositoryNotFound, "Repository not found"}
    };

    public static string GetMessageForCode(ApiErrorCode code)
    {
        return _messages.ContainsKey(code) ? _messages[code] : _messages[ApiErrorCode.Unknown];
    }
}