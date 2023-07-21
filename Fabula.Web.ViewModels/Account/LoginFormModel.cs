namespace Fabula.Web.ViewModels.Account;

using static Common.ValidationConstants.ApplicationUser;

using System.ComponentModel.DataAnnotations;

public class LoginFormModel
{
    public LoginFormModel()
    {
        Utilities = new AuthUtilitiesViewModel();
    }

    //Can either be username or email
    //Shortest possible email is 3 characters long
    //Longest possible email is 320 characters long
    //Thus when validating the login credential in the Login Razor Page we use the email min and max lengths

    [Required]
    [Display(Name = "Username or email")]
    [StringLength(EmailMaxLength, MinimumLength = EmailMinLength)]
    public string LoginCredential { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;


    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }

    public AuthUtilitiesViewModel Utilities { get; set; }
}
