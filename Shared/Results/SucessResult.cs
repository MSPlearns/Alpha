namespace Shared.Results;

public class SucessResult<T> : Result<T> where T : class
{
    public SucessResult( int statusCode, T? data)
    {
        Succeeded = true;
        StatusCode = statusCode;
        Data = data;
    }

    public SucessResult(int statusCode)
    {
        Succeeded = true;
        StatusCode = statusCode;
        Data = null;
    }
}