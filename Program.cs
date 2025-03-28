using System.Security.Claims;
using ClubMates.Web.AppDbContext;
using ClubMates.Web.Areas.Club.Services;
using ClubMates.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppIdentityDbContext>(options => options
.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
    }).AddCookie();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});


builder.Services.AddIdentity<ClubMatesUser, IdentityRole>(Options =>
{
    Options.Password.RequireDigit = true;
    Options.Password.RequireLowercase = true;
    Options.Password.RequireUppercase = true;
    Options.Password.RequiredLength = 6;
    Options.Lockout = new LockoutOptions
    {
        AllowedForNewUsers = true,
        DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5),
        MaxFailedAccessAttempts = 5
    };
    Options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();


builder.Services.AddScoped<IClubLayoutService, ClubLayoutService>();
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("GuestAndSuperAdmin", policy => policy.RequireClaim(ClaimTypes.Role, "SuperAdmin").RequireClaim(ClaimTypes.Role,"Guest"))
    .AddPolicy("MustBeAGuest", policy => policy.RequireClaim(ClaimTypes.Role, "Guest", "ClubUser","SuperAdmin"));



var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "Club",
    pattern: "Club/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
