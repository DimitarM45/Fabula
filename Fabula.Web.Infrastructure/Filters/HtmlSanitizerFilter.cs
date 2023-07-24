namespace Fabula.Web.Infrastructure.Filters;

using Ganss.Xss;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc.Filters;

public class HtmlSanitizerFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.HttpContext.Request.Method == HttpMethod.Post.Method && context.HttpContext.Request.HasFormContentType)
        {
            IHtmlSanitizer? sanitizer =
                    (IHtmlSanitizer?)context.HttpContext.RequestServices.GetService(typeof(IHtmlSanitizer));

            if (sanitizer == null)
                throw new ArgumentNullException(nameof(sanitizer));

            IFormCollection formFields = await context.HttpContext.Request.ReadFormAsync();

            Dictionary<string, StringValues> modifiedFormFields = new Dictionary<string, StringValues>();

            foreach ((string key, StringValues value) in formFields)
                modifiedFormFields[key] = sanitizer.Sanitize(value.ToString());

            context.HttpContext.Request.Form = new FormCollection(modifiedFormFields);
        }

        await next();
    }
}
