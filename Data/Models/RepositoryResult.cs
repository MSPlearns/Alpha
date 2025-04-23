namespace Data.Models;

public class RepositoryResult<T> : Result where T : class
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
    public static RepositoryResult<T> Updated(T? data)
    {
        return new RepositoryResult<T>
        {
            Suceeded = true,
            StatusCode = 201,
            Data = data
        };
    }

}




