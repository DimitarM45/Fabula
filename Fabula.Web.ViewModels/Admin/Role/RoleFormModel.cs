namespace Fabula.Web.ViewModels.Admin.Role;

using static Common.ValidationConstants.Role;
using static Common.Messages.ErrorMessages.Shared;

using System.ComponentModel.DataAnnotations;

public class RoleFormModel
{
    public string? Id { get; set; }

    [Required(AllowEmptyStrings = false,
        ErrorMessage = StringRequiredErrorMessage)]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength,
        ErrorMessage = StringLengthErrorMessage)]

    public string Name { get; set; } = null!;
}
