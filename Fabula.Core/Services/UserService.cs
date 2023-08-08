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
            WebsiteUrl = user.WebsiteURL,
            Followers = user.Followers.Count(),
            FavoriteCompositions = user.FavoriteCompositions.Count(),
            CreatedLists = user.CreatedLists.Count(),
            FollowedLists = user.FollowedLists.Count(),
            Ratings = user.Ratings.Count(),
            WrittenCompositions = user.Ratings.Count()
        };

        return userProfile;
    }

    public async Task<int> GetCountAsync()
    {
        int count = await dbContext.Users.CountAsync();

        return count;
    }
}
