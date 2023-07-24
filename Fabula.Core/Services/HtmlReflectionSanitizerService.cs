namespace Fabula.Core.Services;

using Contracts;
using Web.ViewModels.Contracts;

using Ganss.Xss;

using System.Reflection;

/// <summary>
/// A service that uses Ganss' HTMLSanitizer and reflection to sanitize all from input data including nested entities' properties automatically. The object that is to undergo
/// sanitization must implement IHtmlSanitizable.
/// </summary>

public class HtmlReflectionSanitizerService : IHtmlReflectionSanitizerService
{
    private readonly IHtmlSanitizer sanitizer;

    public HtmlReflectionSanitizerService(IHtmlSanitizer sanitizer)
    {
        this.sanitizer = sanitizer;
    }

    /// <summary>
    /// Sanitizes the string properties of the given model. Used to protect against possible XSS attacks through form inputs.
    /// </summary>
    /// <param name="sanitizable">The model to sanitize</param>
    /// <returns></returns>

    public void SanitizeModel(IHtmlSanitizable sanitizable)
    {
        foreach (PropertyInfo propInfo in sanitizable.GetSanitizableProperties())
        {
            bool isStringOrNullableString = propInfo.PropertyType == typeof(string) || (propInfo.PropertyType == typeof(string)
                                                                  && (Nullable.GetUnderlyingType(propInfo.PropertyType) != null));

            if (isStringOrNullableString)
            {
                string? value = (string?)propInfo.GetValue(sanitizable);

                if (value != null)
                {
                    string sanitizedValue = sanitizer.Sanitize(value);

                    propInfo.SetValue(sanitizable, sanitizedValue);
                }
            }
        }
    }
}
