﻿namespace Fabula.Web.Infrastructure.Middlewares;

using Ganss.Xss;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

using System.Threading.Tasks;

/// <summary>
/// Custom middleware that uses <see cref="HtmlSanitizer"/> to sanitize form fields from POST requests against XSS attacks.
/// </summary>

public class HtmlSanitizerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.Request.Method == HttpMethod.Post.Method && context.Request.HasFormContentType)
        {
            IHtmlSanitizer? sanitizer =
                    (IHtmlSanitizer?)context.RequestServices.GetService(typeof(IHtmlSanitizer));

            if (sanitizer == null)
                throw new ArgumentNullException(nameof(sanitizer));

            IFormCollection formFields = await context.Request.ReadFormAsync();

            Dictionary<string, StringValues> modifiedFormFields = new Dictionary<string, StringValues>();

            foreach ((string key, StringValues value) in formFields)
            {
                ICollection<string?> stringValues = new List<string?>();

                foreach (var valueString in value)
                    stringValues.Add(sanitizer.Sanitize(valueString));

                StringValues modifiedStringValues = new StringValues(stringValues.ToArray());

                modifiedFormFields[key] = modifiedStringValues;
            }

            context.Request.Form = new FormCollection(modifiedFormFields);
        }

        await next(context);
    }
}
