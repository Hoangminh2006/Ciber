using Ciber.EntityFramework.EntityFramework;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using NLog;
using NLog.Web;
using Ciber.Models;
using Ciber.Manager;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel
                (Microsoft.Extensions.Logging.LogLevel.Error);
builder.Host.UseNLog();

//other classes that need the logger 
builder.Services.AddTransient<GenericHelper>();
builder.Services.AddSingleton<ILog, LogNLog>();
// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<CiberDbContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<CiberDbContext>()
        .AddDefaultTokenProviders();
builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("AdminRole",
        authBuilder =>
        {
            authBuilder.RequireRole("AdminRole");
        });

    options.AddPolicy("UserRole",
        authBuilder =>
        {
            authBuilder.RequireRole("UserRole");
        });
});
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x=>x.LoginPath="/Login/Login");
builder.Services.AddScoped<IOrderManager, OrderManager>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
var app = builder.Build();

var loggerFactory = app.Services.GetService<ILoggerFactory>();
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
app.UseCookiePolicy();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
