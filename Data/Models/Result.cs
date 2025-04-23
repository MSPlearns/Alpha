namespace Data.Models;
public abstract class Result
{
    public bool Suceeded { get; protected set; }
    public int StatusCode { get; protected set; }
    public string? ErrorMessage { get; protected set; }

    public static Result Ok()
    {
        return new SucessResult(200);
    }

    public static Result BadRequest(string? message)
    {
        return new ErrorResult(400, message);
    }

    public static Result NotFound(string? message)
    {
        return new ErrorResult(404, message);
    }

    public static Result AlreadyExists(string? message)
    {
        return new ErrorResult(409, message);
    }

}



