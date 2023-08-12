namespace Fabula.Core.Services;

using Data;
using Contracts;
using Data.Models;
using Web.ViewModels.User;
using Web.ViewModels.Admin.User;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor context;

    private readonly FabulaDbContext dbContext;

    private readonly UserManager<ApplicationUser> userManager;

    public UserService(IHttpContextAccessor context,
        FabulaDbContext dbContext,
        UserManager<ApplicationUser> userManager)
    {
        this.context = context;
        this.dbContext = dbContext;
        this.userManager = userManager;
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
            .Include(u => u.WrittenCompositions)
            .Include(u => u.FavoriteCompositions)
            .Include(u => u.FollowedLists)
            .Include(u => u.Ratings)
            .Include(u => u.CreatedLists)
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
            WrittenCompositions = user.WrittenCompositions.Count()
        };

        return userProfile;
    }

    public async Task<int> GetCountAsync()
    {
        int count = await dbContext.Users.CountAsync();

        return count;
    }

    public async Task<IEnumerable<UserDashboardViewModel>> GetAllForAdminDashboardAsync()
    {
        IEnumerable<UserDashboardViewModel> userDashboardViewModels = await userManager.Users
            .AsNoTracking()
            .Select(u => new UserDashboardViewModel()
            {
                Id = u.Id.ToString(),
                Username = u.UserName,
                ProfilePictureUrl = u.ProfilePictureUrl,
                WrittenCompositions = u.WrittenCompositions.Count(),
            })
            .ToListAsync();

        foreach (UserDashboardViewModel userViewModel in userDashboardViewModels)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(userViewModel.Id);

            if (user != null)
                userViewModel.Role = (await userManager.GetRolesAsync(user))[0];
        }

        return userDashboardViewModels;
    }

    public async Task<string?> GetUsernameAsync(string userId)
    {
        ApplicationUser? user = await userManager.FindByIdAsync(userId);

        if (user == null)
            return null;

        return user.UserName;
    }

    public async Task AddRoleToUserAsync(string userId, string roleId)
    {
        ApplicationUser? user = await userManager.FindByIdAsync(userId);

        if (user == null)
            return;

        IdentityRole<Guid>? role = await dbContext.Roles.FirstOrDefaultAsync(r => r.Id.ToString() == roleId);

        if (role == null)
            return;

        await userManager.AddToRoleAsync(user, role.Name);
    }

    public async Task AddFavoriteGenreToUserAsync(string userId, int genreId)
    {
        ApplicationUser? user = await userManager.FindByIdAsync(userId);

        if (user == null)
            return;

        Genre? genre = await dbContext.Genres.FirstOrDefaultAsync(g => g.Id == genreId);

        if (genre == null)
            return;

        UserFavoriteGenre userGenre = new UserFavoriteGenre()
        {
            UserId = Guid.Parse(userId),
            GenreId = genreId
        };

        await dbContext.UsersFavoriteGenres.AddAsync(userGenre);

        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveFavoriteGenreFromUserAsync(string userId, int genreId)
    {
        UserFavoriteGenre? userGenre = await dbContext.UsersFavoriteGenres
            .FirstOrDefaultAsync(ug => ug.UserId.ToString() == userId && ug.GenreId == genreId);

        if (userGenre == null)
            return;

        dbContext.UsersFavoriteGenres.Remove(userGenre);

        await dbContext.SaveChangesAsync();
    }
}
