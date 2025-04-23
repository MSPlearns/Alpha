namespace Shared.Results;

public class ErrorResult : Result
{
    public ErrorResult( int statusCode, string? errorMessage)
    {
        Suceeded = false;
        StatusCode = statusCode;
        ErrorMessage = errorMessage;
    }
}