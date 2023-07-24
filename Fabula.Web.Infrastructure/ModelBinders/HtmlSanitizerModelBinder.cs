namespace Fabula.Web.Infrastructure.ModelBinders;

using Core.Contracts;
using ViewModels.Contracts;

using Microsoft.AspNetCore.Mvc.ModelBinding;

using System.Threading.Tasks;

public class HtmlSanitizerModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext? bindingContext)
    {
        if (bindingContext == null)
            throw new ArgumentNullException(nameof(bindingContext));

        BindModelAsync(bindingContext).ContinueWith(action =>
        {
            IHtmlReflectionSanitizerService? sanitizer =
                (IHtmlReflectionSanitizerService?)bindingContext.HttpContext.RequestServices.GetService(typeof(IHtmlReflectionSanitizerService));

            IHtmlSanitizable? sanitizable = bindingContext as IHtmlSanitizable;

            if (sanitizer != null && sanitizable != null)
                sanitizer.SanitizeModel(sanitizable);
        });

        return Task.CompletedTask;
    }
}
