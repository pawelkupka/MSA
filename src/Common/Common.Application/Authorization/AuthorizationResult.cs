namespace Common.Application.Authorization;

public record AuthorizationResult(bool IsAuthorized, string ErrorMessage)
{
    public static AuthorizationResult Authorized() => new AuthorizationResult(true, null);
    public static AuthorizationResult Unauthorized(string errorMessage = "Access Denied") => new AuthorizationResult(false, errorMessage);
}