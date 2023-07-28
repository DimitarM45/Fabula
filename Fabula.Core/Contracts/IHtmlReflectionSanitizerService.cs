namespace Fabula.Core.Contracts;

public interface IHtmlReflectionSanitizerService
{
    void SanitizeModel(Type modelType, object model);
}
