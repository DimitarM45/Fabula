namespace Fabula.Web.ViewModels.Account;

using static Common.Messages.ErrorMessages.Shared;
using static Common.ValidationConstants.ApplicationUser;
using static Common.Messages.ErrorMessages.Authentication;

using System.ComponentModel.DataAnnotations;

public class RegisterFormModel
{
    public RegisterFormModel()
    {
        Utilities = new AuthUtilitiesViewModel();
    }

    [Display(Name = "First name")]
    [Required(AllowEmptyStrings = false, 
        ErrorMessage = StringRequiredErrorMessage)]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength, 
        ErrorMessage = StringLengthErrorMessage)]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last name")]
    [Required(AllowEmptyStrings = false, 
        ErrorMessage = StringRequiredErrorMessage)]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength, 
        ErrorMessage = StringLengthErrorMessage)]
    public string LastName { get; set; } = null!;

    [Required(AllowEmptyStrings = false, 
        ErrorMessage = StringRequiredErrorMessage)]
    [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength, 
        ErrorMessage = StringLengthErrorMessage)]
    public string Username { get; set; } = null!;

    [Display(Name = "Date of birth")]
    [Required(AllowEmptyStrings = false, 
        ErrorMessage = StringRequiredErrorMessage)]
    public string Birthdate { get; set; } = null!;

    public DateTime? ParsedBirthdate { get; set; }

    [Display(Name = "Email")]
    [EmailAddress(ErrorMessage = InvalidStringErrorMessage)]
    [Required(AllowEmptyStrings = false, 
        ErrorMessage = StringRequiredErrorMessage)]
    [StringLength(EmailMaxLength, MinimumLength = EmailMinLength, 
        ErrorMessage = StringLengthErrorMessage)]
    public string Email { get; set; } = null!;

    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 6, 
        ErrorMessage = StringLengthErrorMessage)]
    [Required(AllowEmptyStrings = false, 
        ErrorMessage = StringRequiredErrorMessage)]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", 
        ErrorMessage = PasswordDoesNotMatchErrorMessage)]
    [Required(AllowEmptyStrings = false,
        ErrorMessage = StringRequiredErrorMessage)]
    public string ConfirmPassword { get; set; } = null!;

    public AuthUtilitiesViewModel Utilities { get; set; }
}
