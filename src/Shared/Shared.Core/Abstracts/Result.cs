using Shared.Core.Interfaces;

namespace Shared.Core.Abstracts;

public class Result : IResult
{
    public bool IsSuccess { get; private set; }

    public string? Message { get; private set; }

    public string[]? Errors { get; private set; }

    public static Result Success(string? message = null)
    {
        return new Result
        {
            IsSuccess = true,
            Message = message
        };
    }

    public static Result Failure(string? message = null, string[]? errors = null)
    {
        return new Result
        {
            IsSuccess = false,
            Message = message,
            Errors = errors
        };
    }
}


public class Result<T> : IResult<T>
{
    public T? Value { get; private set; }

    public IEnumerable<T>? Values { get; private set; }

    public bool IsSuccess { get; private set; }

    public string? Message { get; private set; }

    public string[]? Errors { get; private set; }

    public static Result<T> Success(T value, string? message = null)
    {
        return new Result<T>
        {
            Value = value,
            IsSuccess = true,
            Message = message
        };
    }

    public static Result<T> Success(IEnumerable<T> values, string? message = null)
    {
        return new Result<T>
        {
            Values = values,
            IsSuccess = true,
            Message = message
        };
    }

    public static Result<T> Failure(string? message = null, string[]? errors = null)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Message = message,
            Errors = errors
        };
    }
}
