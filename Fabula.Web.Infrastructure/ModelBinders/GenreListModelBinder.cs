namespace Fabula.Web.Infrastructure.ModelBinders;

using Fabula.Web.ViewModels.Composition;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using System.Threading.Tasks;

public class GenreListModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext? bindingContext)
    {
        if (bindingContext == null)
            throw new ArgumentNullException(nameof(bindingContext));

        ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueResult != ValueProviderResult.None && !string.IsNullOrWhiteSpace(valueResult.FirstValue))
        {
            if (bindingContext.ModelType == typeof(CompositionCreateFormModel))
            {
                IEnumerable<int> genresToBind = bindingContext.HttpContext.Request.Form["Genres"]
                    .ToString()
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(n => int.Parse(n));

                CompositionCreateFormModel model = (CompositionCreateFormModel)bindingContext.Model;

                model.Genres = genresToBind;

                bindingContext.ModelState.SetModelValue(
                    bindingContext.ModelName + ".Genres",
                    valueResult);
            }
        }

        return Task.CompletedTask;
    }
}
