namespace Common.Application.Authorization;

public interface IAuthorizationHandler<TRequest> where TRequest : IAuthorizable
{
    Task<AuthorizationResult> Handle(TRequest request, CancellationToken cancellationToken = default);
}