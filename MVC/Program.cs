using System.Drawing;
using BLL.DAL;
using BLL.Models;
using BLL.Services;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("Db");
builder.Services.AddDbContext<Db>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IService<User, UserModel>, UserService>();

var app = builder.Build();


// IoC Container:

builder.Services.AddScoped<IService<Evaluated, EvaluatedModel>, EvaluatedService>(); 

builder.Services.AddScoped<IService<Evaluation, EvaluationModel>, EvaluationService>();

builder.Services.AddScoped<IService<User, UserModel>, UserService>();

//builder.Services.AddHttpContextAccessor();
//builder.Services.AddSingleton<HttpServiceBase, HttpService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Users/Login";
        options.AccessDeniedPath = "/Users/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true;
    });




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
