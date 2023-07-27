namespace Fabula.Core.Contracts;

using System.Reflection;

public interface IHtmlReflectionSanitizerService
{
    void SanitizeModel(Type modelType, object model);
}
