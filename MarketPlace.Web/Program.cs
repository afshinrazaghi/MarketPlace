using GoogleReCaptcha.V3;
using GoogleReCaptcha.V3.Interface;
using MarketPlace.Application.Services;
using MarketPlace.Application.Services.Implementations;
using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.Context;
using MarketPlace.DataLayer.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using MarketPlace.Application;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

#region config services
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();
builder.Services.ConfigureApplicationServices();
#endregion

#region html encoder
builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Arabic }));
#endregion

#region config database
builder.Services.AddDbContext<MarketPlaceDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MarketPlaceConnection")));
#endregion

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("\\wwwroot\\Auth/\\"))
    .SetApplicationName("MarketPlace")
    .SetDefaultKeyLifetime(TimeSpan.FromDays(30));

#region authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/login";
    options.LogoutPath = "/log-out";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
});

#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});



app.Run();
