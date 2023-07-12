namespace Fabula.Web.ViewModels.Tag;

using static Common.ValidationConstants.Tag;

using System.ComponentModel.DataAnnotations;

public class TagFormModel
{
    [Required]
    [MinLength(NameMinLength)]
    [MaxLength(NameMaxLength)]

    public string Name { get; set; } = null!;
}
