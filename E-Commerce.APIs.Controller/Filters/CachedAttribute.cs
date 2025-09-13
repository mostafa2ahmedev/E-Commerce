using E_Commerce.Application.Services.Common.Contracts.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.APIs.Controller.Filters
{
    internal class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSeconds;

        public CachedAttribute(int timeToLiveInSeconds)
        {
            _timeToLiveInSeconds = timeToLiveInSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           var responseCacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
           
           var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

           var response =await responseCacheService.GetCachedResponseAsync(cacheKey);

            if (!string.IsNullOrEmpty(response))
            {
                var result = new ContentResult() {
                        Content = response,
                        ContentType ="application/json",
                        StatusCode = 200,
            };
                context.Result = result;
                return;
            }
            var executedActionContext =  await next.Invoke();
            if (executedActionContext.Result is OkObjectResult objectResult && objectResult.Value is not null) {

                await responseCacheService.CacheResponseAsync(cacheKey, objectResult.Value, TimeSpan.FromSeconds(_timeToLiveInSeconds));
            }
             
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
           //baseurl/api/products/pageindex==18&&pagesize=5&&sort=name

           var keyBuilder= new StringBuilder();

            keyBuilder.Append(request.Path);

            foreach (var (key,value) in request.Query.OrderBy(query=>query.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }   

            return keyBuilder.ToString();

        }
    }
}
