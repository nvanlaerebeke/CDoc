using ApiBase.Error;

namespace CDoc.Error;

public class CDocException : ApiException<ApiErrorCode>
{
    public CDocException(IApiError<ApiErrorCode> error) : base(error)
    {
    }
}