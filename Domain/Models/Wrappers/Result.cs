﻿namespace Domain.Models.Wrappers;

public record Result
{
    public bool Succeed { get; init; }
    public string? Message { get; init; }

    public ErrorTypeCode ErrorCode { get; init; }
    public static Result Successful() => new Result() { Succeed = true };

    public static Result Failed(string message, ErrorTypeCode code)
        => new Result()
        {
            Succeed = false,
            Message = message,
            ErrorCode = code
        };
}

public record Result<T> : Result
{
    public T? Content { get; init; }
    public ErrorTypeCode ErrorCode { get; init; }

    public static Result<T> Successful(T content)
        => new Result<T>()
        {
            Succeed = true,
            Content = content,
        };
    
    public new static Result<T> Failed(string message, ErrorTypeCode code)
        => new Result<T>()
        {
            Succeed = false,
            Message = message,
            ErrorCode = code
        };
}