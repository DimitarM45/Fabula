namespace Fabula.Core.Contracts;

public interface IHtmlReflectionSanitizerService
{
    public void SanitizeModel<TModel>(TModel model);
}
