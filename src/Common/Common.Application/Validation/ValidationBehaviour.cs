using MediatR;

namespace Common.Application.Validation;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IValidable
        where TResponse : ValidationResult, new()
{
    private readonly IValidationHandler<TRequest> _handler;

    public ValidationBehaviour(IValidationHandler<TRequest> handler)
    {
        _handler = handler;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var result = await _handler.Handle(request);
        if (result.IsValid)
        {
            return await next();
        }
        return new TResponse { ErrorMessage = result.ErrorMessage };
    }
}