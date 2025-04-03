using System.Reflection.Metadata.Ecma335;

namespace Application.Wrappers;

public record Result
{
    public bool Succeed { get; init; }
    
    public static Result Successful() => new Result() { Succeed = true };
    
    public static Result Failed() => new Result() { Succeed = false};

}

public record Result<T> : Result
{
    public T Content { get; init; } = default!;

    public static Result<T> Successful(T content)
        => new Result<T>()
        {
            Succeed = true,
            Content = content
        };
}
