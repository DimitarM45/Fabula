namespace Fabula.Core.Services;

using Contracts;

using Microsoft.AspNetCore.Http;

using System.Security.Claims;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor context;

    public UserService(IHttpContextAccessor context)
    {
        this.context = context;
    }

    public string? GetUserId()
    {
        string? userId = context?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        return userId;
    }
}
