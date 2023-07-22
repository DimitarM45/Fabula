namespace Fabula.Core.Services;

using Contracts;

using Ganss.Xss;

using System.Reflection;

/// <summary>
/// A service that uses Ganss' HTMLSanitizer and reflection to sanitize all from input data including nested entities' properties automatically.
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
    /// <typeparam name="TModel">Type of the model</typeparam>
    /// <param name="model">The model to sanitize</param>
    /// <returns></returns>

    public void SanitizeModel<TModel>(TModel model)
    {
        Type modelType = typeof(TModel);

        PropertyInfo[] properties = modelType
            .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

        foreach (PropertyInfo propInfo in properties)
        {
            bool isStringOrNullableString = propInfo.PropertyType == typeof(string) || (propInfo.PropertyType == typeof(string)
                                                                  && (Nullable.GetUnderlyingType(propInfo.PropertyType) != null));

            if (isStringOrNullableString)
            {
                string? value = (string?)propInfo.GetValue(model);

                if (value != null)
                {
                    string sanitizedValue = sanitizer.Sanitize(value);

                    propInfo.SetValue(model, sanitizedValue);
                }
            }
        }
    }
}
