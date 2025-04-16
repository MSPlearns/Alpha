namespace Data.Models;
public abstract class RepositoryResult
{
    public bool Suceeded { get; protected set; }
    public int StatusCode { get; protected set; }
    public string? ErrorMessage { get; protected set; }

    public static RepositoryResult Ok()
    {
        return new SucessResult(200);
    }

    public static RepositoryResult BadRequest(string? message)
    {
        return new ErrorResult(400, message);
    }

    public static RepositoryResult NotFound(string? message)
    {
        return new ErrorResult(404, message);
    }

    public static RepositoryResult AlreadyExists(string? message)
    {
        return new ErrorResult(409, message);
    }

}

public class RepositoryResult<T> : RepositoryResult where T : class
{
    public T? Data { get; private set; }

    public static RepositoryResult<T> Ok(T? data)
    {
        return new RepositoryResult<T>
        {
            Suceeded = true,
            StatusCode = 200,
            Data = data
        };
    }

    public static RepositoryResult<T> Created(T? data)
    {
        return new RepositoryResult<T>
        {
            Suceeded = true,
            StatusCode = 201,
            Data = data
        };
    }

}




