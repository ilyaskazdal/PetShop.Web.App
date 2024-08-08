using Microsoft.EntityFrameworkCore;
using PS.Data.Abstract;
using PS.Data.Concrete.EfCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PSContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("PS.Web.UI")));
builder.Services.AddScoped<IContactRepo, EfContactRepo>();
builder.Services.AddScoped<IProductRepo, EfProductRepo>();
builder.Services.AddScoped<ICategoryRepo, EfCategoryRepo>();
builder.Services.AddScoped<IProductTypeRepo, EfProductTypeRepo>();
builder.Services.AddScoped<IUserRepo, EfUserRepo>();
builder.Services.AddScoped<IAdoptRepo, EfAdoptRepo>();  

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

var app = builder.Build();

SeedData.TestVerileriDoldur(app);

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
    name: "areaRoute",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "MarketProducts",
    pattern: "Market/Details/{id?}",
    defaults: new { controller = "Market", action = "Details" }
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
