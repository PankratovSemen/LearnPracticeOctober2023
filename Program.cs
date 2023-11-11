using Microsoft.EntityFrameworkCore;
using LearnPractice.Models;
using LearnPractice.Models.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using LearnPractice.Areas.Identity.Data;
using LearnPractice.Data;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ArticlesContext>(options => options.UseSqlServer(connection));

builder.Services.AddDefaultIdentity<LearnPracticeUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LearnPracticeContext>();


builder.Services.AddDbContext<ArticlesContext>(options => options.UseSqlServer(connection));
builder.Services.AddDbContext<LearnPracticeContext>(options => options.UseSqlServer(connection));

builder.Services.AddDbContext<CarsContext>(options => options.UseSqlServer(connection));



// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseAuthentication();;

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();
