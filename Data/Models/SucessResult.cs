namespace Data.Models;

public class SucessResult : RepositoryResult
{
    public SucessResult( int statusCode)
    {
        Suceeded = true;
        StatusCode = statusCode;
    }
}




