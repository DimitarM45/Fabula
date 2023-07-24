namespace Fabula.Web.Infrastructure.ModelBinders;

using ViewModels.Contracts;

using Microsoft.AspNetCore.Mvc.ModelBinding;

public class HtmlSanitizerModelBinderProvider : IModelBinderProvider
{

    public IModelBinder? GetBinder(ModelBinderProviderContext? context)
    {
        if (context == null)
            throw new ArgumentNullException(nameof(context));

        if (context.Metadata.ModelType.GetInterfaces().FirstOrDefault(i => i.GetType() == typeof(IHtmlSanitizable)) != null)
            return new HtmlSanitizerModelBinder();

        return null;
    }
}
