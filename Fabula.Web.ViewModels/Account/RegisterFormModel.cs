namespace Fabula.Web.ViewModels.Account;

using static Common.ValidationConstants.ApplicationUser;

using System.ComponentModel.DataAnnotations;

public class RegisterFormModel
{
    public RegisterFormModel()
    {
        Utilities = new AuthUtilitiesViewModel();
    }

    [Required]
    [Display(Name = "First name")]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string FirstName { get; set; } = null!;

    [Required]
    [Display(Name = "Last name")]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string LastName { get; set; } = null!;

    [Required]
    [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength)]
    public string Username { get; set; } = null!;

    [Display(Name = "Date of birth")]
    [Required(ErrorMessage = "Birthdate is required.")]
    public string Birthdate { get; set; } = null!;

    public DateTime? ParsedBirthdate { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    [StringLength(EmailMaxLength, MinimumLength = EmailMinLength)]
    public string Email { get; set; } = null!;

    [Required]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = null!;

    public AuthUtilitiesViewModel Utilities { get; set; }
}
