namespace Data.Models;

public class SucessResult : Result
{
    public SucessResult( int statusCode)
    {
        Suceeded = true;
        StatusCode = statusCode;
    }
}




