using MarketPlace.Application.Services;
using MarketPlace.Application.Services.Implementations;
using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.Context;
using MarketPlace.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

#region config services
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHelper, PasswordHelper>();

#endregion

#region html encoder
builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Arabic }));
#endregion

#region config database
builder.Services.AddDbContext<MarketPlaceDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MarketPlaceConnection")));
#endregion
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
