using System.Net;
using ApiBase.Error;

namespace CDoc.Error;

public class ApiError : IApiError<ApiErrorCode> {

    public ApiError(ApiErrorCode errorCode) : this(errorCode, HttpStatusCode.InternalServerError) { }

    public ApiError(ApiErrorCode errorCode, HttpStatusCode httpStatusCode) {
        Code = errorCode;
        Message = ApiErrorMessage.GetMessageForCode(errorCode);
        HttpStatusCode = httpStatusCode;
    }

    public ApiErrorCode Code { get; }

    public string Message { get; }

    public HttpStatusCode HttpStatusCode { get; }
}