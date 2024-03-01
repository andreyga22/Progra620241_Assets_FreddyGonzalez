using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Progra620241_Assets_FreddyGonzalez.Attributes {
    [AttributeUsage(validOn: AttributeTargets.All)]
    public sealed class ApiKeyAttribute : Attribute, IAsyncActionFilter {

        private readonly string _apiKey = "P6ApiKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
            if (!context.HttpContext.Request.Headers.TryGetValue(_apiKey, out var ApiSalida)) {
                context.Result = new ContentResult() {
                    StatusCode = 401,
                    Content = "The http request doesn't contain security information"
                };
                return;
            }
            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKeyValue = appSettings.GetValue<string>(_apiKey);
            if (apiKeyValue != null && !apiKeyValue.Equals(ApiSalida)) {
                context.Result = new ContentResult() {
                    StatusCode = 401,
                    Content = "Incorret ApiKey Data"
                };
                return;
            }

            await next();
        }

    }
}
