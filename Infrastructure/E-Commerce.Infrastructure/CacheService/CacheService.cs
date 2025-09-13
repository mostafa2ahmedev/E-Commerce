using E_Commerce.Application.Services.Common.Contracts.Infrastructure;
using E_Commerce.Domain.Contracts.Infrastructure;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.CacheServices
{
    internal class CacheService : IResponseCacheService
    {
        private readonly IDatabase _database;

        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task CacheResponseAsync(string key, object response, TimeSpan duration)
        {
            if (response is null) return;

            var serializedOptions = new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            
            var serializedResponse = JsonSerializer.Serialize(response, serializedOptions);

            await _database.StringSetAsync(key, serializedResponse,duration);
        }

        public async Task<string?> GetCachedResponseAsync(string key)
        {
            var response = await _database.StringGetAsync(key);

            if (response.IsNull) return null;
            return response;
        }
    }
}
