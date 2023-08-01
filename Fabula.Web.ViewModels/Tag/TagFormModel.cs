namespace Fabula.Web.ViewModels.Tag;

using static Common.ValidationConstants.Tag;
using static Common.Messages.ErrorMessages.Shared;

using System.ComponentModel.DataAnnotations;

public class TagFormModel
{
    [Required(AllowEmptyStrings = false, 
        ErrorMessage = StringRequiredErrorMessage)]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength, 
        ErrorMessage = StringLengthErrorMessage)]

    public string Name { get; set; } = null!;
}
