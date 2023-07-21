namespace Fabula.Web.ViewModels.Account;

using Microsoft.AspNetCore.Authentication;

public class AuthUtilitiesViewModel
{
    public string? ReturnUrl { get; set; } = null!;

    public IList<AuthenticationScheme>? ExternalLogins { get; set; }
}
