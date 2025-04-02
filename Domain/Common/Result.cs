namespace Domain.Common;

public static class Result
{
	public static Result<T> Success<T>(T value) => new Result<T> { Value = value };
	public static Result<T> Failure<T>(ErrorResult error) => new Result<T> { Error = error };
}

public record Result<T>
{
	public T? Value { get; set; } = default!;
	public ErrorResult? Error { get; set; }
	
	public bool IsSuccess => Error is null;
}