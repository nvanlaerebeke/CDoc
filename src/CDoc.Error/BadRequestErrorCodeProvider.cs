using ApiBase.Error;

namespace CDoc.Error;

public class BadRequestErrorCodeProvider : IBadRequestErrorCodeProvider {
    public string GetCode(string property) {
        if (Enum.TryParse<ApiErrorCode>($"Invalid{property}", out var code)) {
            return code.ToString();
        }
        return nameof(ApiErrorCode.Unknown);
    }

    public string GetMessageForCode(string code) {
        if (Enum.TryParse<ApiErrorCode>(code, out var apiErrorCode)) {
            return ApiErrorMessage.GetMessageForCode(apiErrorCode);
        }
        return ApiErrorMessage.GetMessageForCode(ApiErrorCode.InvalidValue);
    }
}