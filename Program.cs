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
using DotNetEnv;
//Add the Logging
using Microsoft.Extensions.Logging;

using Softka.Models;
using FluentValidation;
using Softka.Validators;

using DinkToPdf;
using DinkToPdf.Contracts;
using Softka.Utils.Extention;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var Context = new CustomAssemblyLoad();
var path = Context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "C:/Users/fjgt2/OneDrive/Escritorio/Softka_Riwi/Softka/Utils/LibreryPdf/libwkhtmltox.dll"));
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

System.Console.WriteLine($"cargando archivo path: {path}");

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

builder.Services.Configure<Email>(builder.Configuration.GetSection("EmailSettings"));

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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JwtToken"))
    };
});
//add the Scooped of JWT
builder.Services.AddScoped<IJwtRepository, JwtRepository>();
builder.Services.AddScoped<Bcrypt>(); 

//Add the Scooped of Method GeAll
builder.Services.AddScoped<IUserRepository, UserRepository>();
// we configured teh logger
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();


builder.Services.AddTransient<IValidator<User>, UserValidator>(); //service to validate models

builder.Services.AddTransient<MailRepository>();


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
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
