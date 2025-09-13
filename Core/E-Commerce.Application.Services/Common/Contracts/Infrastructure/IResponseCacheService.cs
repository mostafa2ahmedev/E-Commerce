namespace E_Commerce.Application.Services.Common.Contracts.Infrastructure
{
    public interface IResponseCacheService
    {

        Task CacheResponseAsync(string key,object response,TimeSpan duration);

        Task<string?> GetCachedResponseAsync(string key);
    }
}
