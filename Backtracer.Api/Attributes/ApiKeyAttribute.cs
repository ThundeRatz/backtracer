using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Backtracer.Api.Attributes;

[AttributeUsage(validOn: AttributeTargets.Class)]
internal class ApiKeyAttribute : Attribute, IAsyncActionFilter {
    private const string APIKEYNAME = "ApiKey";
    public const string APIKEYHEADER = "X-Api-Key";

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
        if (!context.HttpContext.Request.Headers.TryGetValue(APIKEYHEADER, out var extractedApiKey)) {
            context.Result = new ContentResult() {
                StatusCode = 401,
                Content = "Api Key was not provided"
            };
            return;
        }

        var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

        var apiKey = appSettings.GetValue<string>(APIKEYNAME);

        if (!apiKey.Equals(extractedApiKey)) {
            context.Result = new ContentResult() {
                StatusCode = 401,
                Content = "Api Key is not valid"
            };
            return;
        }

        await next();
    }
}
