using System.Text.Json.Serialization;

namespace CodeD.Domain.Abstractions;

public sealed class Result
{
    public static readonly Result Ok = new(Error.None);

    public bool IsSuccess => Error == Error.None;
    public Error Error { get; private set; }

    private Result(Error error)
    {
        Error = error;
    }

    public static Result<T> Success<T>(T value)
    {
        return Result<T>.Success(value);
    }

    public static Result Failure(Error error)
    {
        return new Result(error);
    }

    public static Result<T> Failure<T>(Error error)
    {
        return Result<T>.Failure(error);
    }
}

public sealed class Result<T>
{
    public bool IsSuccess => Error == Error.None;
    public Error Error { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T Value { get; }

    private Result(Error error)
    {
        Value = default!;
        Error = error;
    }

    private Result(T value)
    {
        Value = value;
        Error = Error.None;
    }

    internal static Result<T> Success(T value)
    {
        return new Result<T>(value);
    }

    internal static Result<T> Failure(Error error)
    {
        return new Result<T>(error);
    }

    public static implicit operator Result(Result<T> value)
    {
        if (value.IsSuccess)
            return Result.Ok;
        return Result.Failure(value.Error);
    }

    public static explicit operator Result<T>(Result value)
    {
        if (value.IsSuccess)
            throw new InvalidCastException();
        return Result<T>.Failure(value.Error);
    }
}