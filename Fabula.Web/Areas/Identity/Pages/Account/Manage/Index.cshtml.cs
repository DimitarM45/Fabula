namespace Fabula.Web.Areas.Identity.Pages.Account.Manage;

#nullable disable

using Data.Models;

using static Common.Messages.ErrorMessages.Shared;
using static Common.ValidationConstants.ApplicationUser;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

public class IndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IndexModel(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public string Username { get; set; }
    
    public string ProfilePictureUrl { get; set; }

    public string WebsiteUrl { get; set; }

    public string Bio { get; set; }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Display(Name = "Phone number")]
        [Phone(ErrorMessage = InvalidStringErrorMessage)]

        public string PhoneNumber { get; set; }

        [Display(Name = "Profile picture url")]
        [Url(ErrorMessage = InvalidStringErrorMessage)]

        public string ProfilePictureUrl { get; set; }

        [StringLength(BioMaxLength, MinimumLength = BioMinLength,
            ErrorMessage = StringLengthErrorMessage)]

        public string Bio { get; set; }

        [Display(Name = "Website url")]
        [Url(ErrorMessage = InvalidStringErrorMessage)]

        public string WebsiteUrl { get; set; }
    }

    private async Task LoadAsync(ApplicationUser user)
    {
        string userName = await _userManager.GetUserNameAsync(user);

        string phoneNumber = await _userManager.GetPhoneNumberAsync(user);

        Username = userName;

        Input = new InputModel
        {
            PhoneNumber = phoneNumber,
            ProfilePictureUrl = user.ProfilePictureUrl,
            Bio = user.Bio,
            WebsiteUrl = user.WebsiteURL
        };
    }

    public async Task<IActionResult> OnGetAsync()
    {
        ApplicationUser user = await _userManager.GetUserAsync(User);

        if (user == null)
            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });

        await LoadAsync(user);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ApplicationUser user = await _userManager.GetUserAsync(User);

        if (user == null)
            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });

        if (!ModelState.IsValid)
        {
            await LoadAsync(user);

            return Page();
        }

        string phoneNumber = await _userManager.GetPhoneNumberAsync(user);

        if (Input.PhoneNumber != phoneNumber ||
            Input.ProfilePictureUrl != user.ProfilePictureUrl ||
            Input.Bio != user.Bio ||
            Input.WebsiteUrl != user.WebsiteURL)
        {
            IdentityResult setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);

            if (!setPhoneResult.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to set phone number.";

                return RedirectToPage();
            }

            user.Bio = Input.Bio;

            IdentityResult setBioResult = await _userManager.UpdateAsync(user);

            if (!setBioResult.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to set bio.";

                return RedirectToPage();
            }

            user.ProfilePictureUrl = Input.ProfilePictureUrl;

            IdentityResult setProfilePictureUrlResult = await _userManager.UpdateAsync(user);

            if (!setProfilePictureUrlResult.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to set profile picture url.";

                return RedirectToPage();
            }

            user.WebsiteURL = Input.WebsiteUrl;

            IdentityResult setWebsiteUrlResult = await _userManager.UpdateAsync(user);

            if (!setProfilePictureUrlResult.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to set website url.";

                return RedirectToPage();
            }
        }

        await _signInManager.RefreshSignInAsync(user);

        StatusMessage = "Your profile has been updated";

        return RedirectToPage();
    }
}
