namespace Shared.Results;

public class SucessResult : Result
{
    public SucessResult( int statusCode)
    {
        Suceeded = true;
        StatusCode = statusCode;
    }
}