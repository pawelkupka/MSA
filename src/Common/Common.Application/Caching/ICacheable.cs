namespace Common.Application.Caching;

public interface ICacheable
{
    string CacheKey { get; }
}