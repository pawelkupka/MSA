namespace Common.Application.Authorization;

public record AuthorizationResult(bool IsAuthorized, string ErrorMessage)
{
    public static AuthorizationResult Success() => new AuthorizationResult(true, null);
    public static AuthorizationResult Fail(string errorMessage) => new AuthorizationResult(false, errorMessage);
}