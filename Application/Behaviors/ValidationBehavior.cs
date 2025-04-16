using Application.Wrappers;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            validators.Select(v =>
                v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(r => r.Errors.Count != 0)
            .SelectMany(r => r.Errors)
            .Select(r => r.ErrorMessage)
            .ToList();

        if (failures.Count == 0)
        {
            return await next();
        }

        var message = string.Join(" ", failures);
        return ToResultResponse<TResponse>(message, ErrorTypeCode.ValidationError);
      
        
       
    }
    private static TResponseType ToResultResponse<TResponseType>(string message, ErrorTypeCode code)
        where TResponseType : Result
    {
        
        if (typeof(TResponseType) == typeof(Result))
        {
            return (TResponseType)Result.Failed(message, code);
        }

        var result = typeof(Result<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResponseType).GenericTypeArguments[0])
            .GetMethods()
            .First(m => m.Name.Equals(nameof(Result.Failed)))
            .Invoke(null, [message, code])!;

        return (TResponseType)result;
    }
}