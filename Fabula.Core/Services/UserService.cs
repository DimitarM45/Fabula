namespace Fabula.Core.Services;

using Data;
using Contracts;
using Data.Models;
using Web.ViewModels.User;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;
using System.Threading.Tasks;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor context;

    private readonly FabulaDbContext dbContext;

    public UserService(IHttpContextAccessor context,
        FabulaDbContext dbContext)
    {
        this.context = context;
        this.dbContext = dbContext;
    }

    public string? GetUserId()
    {
        string? userId = context?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        return userId;
    }

    public async Task<UserProfileViewModel?> GetProfileAsync(string userId)
    {
        ApplicationUser? user = await dbContext.Users
            .AsNoTracking()
            .Include(u => u.Followers)
            .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

        if (user == null)
            return null;

        UserProfileViewModel userProfile = new UserProfileViewModel()
        {
            Id = user.Id.ToString(),
            Username = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Bio = user.Bio,
            ProfilePictureUrl = user.ProfilePictureUrl,
            WebsiteURL = user.WebsiteURL,
            Followers = user.Followers.Select(f => new UserViewModel()
                {
                    Id = f.Id.ToString(),
                    Username = user.UserName,
                    ProfilePictureUrl = user.ProfilePictureUrl
                })
                .ToList()
        };

        return userProfile;
    }
}
