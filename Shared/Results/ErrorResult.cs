namespace Shared.Results;

public class ErrorResult<T> : Result<T> where T : class
{
    public ErrorResult( int statusCode, string? errorMessage)
    {
        Suceeded = false;
        StatusCode = statusCode;
        ErrorMessage = errorMessage;
    }
}