namespace Fabula.Web.Infrastructure.ModelBinders;

using Fabula.Web.ViewModels.Composition;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class GenreListModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
            throw new ArgumentNullException(nameof(context));

        if (context.Metadata.ModelType == typeof(CompositionCreateFormModel))
            return new GenreListModelBinder();

        return null;
    }
}
 