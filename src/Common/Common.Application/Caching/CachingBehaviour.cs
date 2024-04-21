using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Common.Application.Caching;

public class CachingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheable
{
    private readonly IMemoryCache _cache;

    public CachingBehaviour(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse response;
        if (_cache.TryGetValue(request.CacheKey, out response))
        {
            return response;
        }
        response = await next();
        _cache.Set(request.CacheKey, response);
        return response;
    }
}