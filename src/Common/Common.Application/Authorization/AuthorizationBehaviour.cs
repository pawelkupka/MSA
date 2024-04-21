using MediatR;

namespace Common.Application.Authorization;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IAuthorizable
    where TResponse : AuthorizationResult, new()
{
    private readonly IAuthorizationHandler<TRequest> _handler;

    public AuthorizationBehaviour(IAuthorizationHandler<TRequest> handler)
    {
        _handler = handler;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var result = await _handler.Handle(request);
        if (result.IsAuthorized)
        {
            return await next();
        }
        return new TResponse { ErrorMessage = result.ErrorMessage };
    }
}