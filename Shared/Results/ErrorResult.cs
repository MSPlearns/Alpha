namespace Shared.Results;

public class ErrorResult<T> : Result<T> where T : class
{
    public ErrorResult( int statusCode, string? errorMessage)
    {
        Succeeded = false;
        StatusCode = statusCode;
        ErrorMessage = errorMessage;
    }
}