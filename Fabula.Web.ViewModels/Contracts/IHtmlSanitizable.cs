namespace Fabula.Web.ViewModels.Contracts;

using System.Reflection;

/// <summary>
/// The interface for all HTML sanitizable models. Implementations of <see cref="GetSanitizableProperties"></see> 
/// must return the properties that require HTML sanitization.
/// </summary>

public interface IHtmlSanitizable
{
    public PropertyInfo[] GetSanitizableProperties();
}
