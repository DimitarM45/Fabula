using Fabula.Data;
using Fabula.Data.Models;
using Fabula.Core.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Fabula.Web.Infrastructure.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<FabulaDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount =
        builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");

    options.Password.RequireLowercase =
        builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");

    options.Password.RequireUppercase =
        builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");

    options.Password.RequireDigit =
        builder.Configuration.GetValue<bool>("Identity:Password:RequireDigit");

    options.Password.RequireNonAlphanumeric =
        builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");

    options.Password.RequiredLength =
        builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
})
.AddRoles<IdentityRole<Guid>>()
.AddEntityFrameworkStores<FabulaDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddApplicationServices(typeof(GenreService))
    .AddOtherServices()
    .AddThirdPartyServices();

builder.Services.AddAuthentication()
.AddFacebook(options =>
{
    options.AppId =
        builder.Configuration.GetValue<string>("Authentication:Facebook:AppId");

    options.AppSecret =
        builder.Configuration.GetValue<string>("Authentication:Facebook:AppSecret");
});

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

builder.Services.AddCors(setup =>
{
    setup.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(
            builder.Configuration.GetValue<string>("CorsAllowedHosts"));
    });
});

builder.Services.AddControllersWithViews();

WebApplication app = builder.Build();

// To view the custom error pages - switch to Production or remove the conditions and only leave the second code block.

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseStatusCodePagesWithRedirects("/error?statusCode={0}");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors();

app.UseMiddleware(typeof(HtmlSanitizerMiddleware));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Dashboard}/{id?}");
app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();
