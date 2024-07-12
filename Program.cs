using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using Softka.Infrastructure.Data;
using Softka.Utils.PasswordHashing;

using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

Env.Load();

//service to BaseContext
builder.Services.AddDbContext<BaseContext>(opt => 
                opt.UseMySql(
                    builder.Configuration.GetConnectionString("DbConnection"),
                    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql")));

//Service to Login Google
builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    opt.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options => {
    options.ClientId = @Environment.GetEnvironmentVariable("ClientId");
    options.ClientSecret = @Environment.GetEnvironmentVariable("ClientSecret");
});

builder.Services.AddScoped<Bcrypt>(); 

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
app.UseCookiePolicy();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
