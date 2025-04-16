namespace Data.Models;

public class ErrorResult : RepositoryResult
{
    public ErrorResult( int statusCode, string? errorMessage)
    {
        Suceeded = false;
        StatusCode = statusCode;
        ErrorMessage = errorMessage;
    }
}




