namespace Shared.Core.Interfaces;

public interface IResult
{
    bool IsSuccess { get; }
    string? Message { get; }
    string[]? Errors { get; }
}

public interface IResult<out T> : IResult
{
    T? Value { get; }
    IEnumerable<T>? Values { get; }
}
