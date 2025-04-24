namespace Shared.Results;
public abstract class Result<T> where T : class
    //TODO: Add an IResult interface

    //Note: There is better ways to implement this, i.e. using a Unit class for void results, but this is good enough for now
{
    public bool Suceeded { get; protected set; }
    public int StatusCode { get; protected set; }
    public string? ErrorMessage { get; protected set; }
    public T? Data { get; protected set; }

    public static Result<T> Ok(T? data)
    {
        return new SucessResult<T>(200, data);
    }

    public static Result<T> Ok()
    {
        return new SucessResult<T>(200);
    }

    public static Result<T> Created()
    {
        return new SucessResult<T>(201);
    }

    public static Result<T> Updated()
    {
        return new SucessResult<T>(202);
    }

    public static Result<T> Deleted()
    {
        return new SucessResult<T>(203);
    }

    public static Result<T> BadRequest(string? message)
    {
        return new ErrorResult<T>(400, message);
    }
    public static Result<T> NotFound(string? message)
    {
        return new ErrorResult<T>(404, message);
    }
    public static Result<T> AlreadyExists(string? message)
    {
        return new ErrorResult<T>(409, message);
    }
    public static Result<T> InternalError(string? message)
    {
        return new ErrorResult<T>(500, message);
    }
}
