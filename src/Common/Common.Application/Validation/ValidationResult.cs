namespace Common.Application.Validation;

public record ValidationResult
{
    public ValidationResult(bool isValid, string errorMessage)
    {
        IsValid = isValid;
        ErrorMessage = errorMessage;
    }

    public bool IsValid { get; init; }
    public string ErrorMessage { get; init; }

    public static ValidationResult Success() => new ValidationResult(true, null);
    public static ValidationResult Fail(string errorMessage) => new ValidationResult(false, errorMessage);
}