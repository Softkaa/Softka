using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Softka.Infrastructure.Data;
using Softka.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Softkat.Services;
using Softka.Utils.PasswordHashing;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

//add JWT settings
builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => 
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = @Environment.GetEnvironmentVariable("Issuer"), 
        ValidAudience = @Environment.GetEnvironmentVariable("Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("djnjcnrjcclmedleñdmededb-ndnuenduenduedyexbgbe"))
    };
});
//add the Scooped of JWT
builder.Services.AddScoped<IJwtRepository, JwtRepository>();
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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
