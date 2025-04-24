namespace Shared.Results;

public class SucessResult<T> : Result<T> where T : class
{
    public SucessResult( int statusCode, T? data)
    {
        Suceeded = true;
        StatusCode = statusCode;
        Data = data;
    }

    public SucessResult(int statusCode)
    {
        Suceeded = true;
        StatusCode = statusCode;
        Data = null;
    }
}