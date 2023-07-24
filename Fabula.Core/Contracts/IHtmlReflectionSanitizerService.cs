namespace Fabula.Core.Contracts;

using Web.ViewModels.Contracts;

public interface IHtmlReflectionSanitizerService
{
    public void SanitizeModel(IHtmlSanitizable model);
}
