using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyMVCMappingDEMO.Data;
using MyMVCMappingDEMO;
using Microsoft.AspNetCore.Identity.UI.Services;
using MyMVCMappingDEMO.Models;
using MyMVCMappingDEMO.Areas.Identity.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MechEmpDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.AddDbContext<MyMVCMappingDEMOContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyMVCMappingDEMOContextConnection")));
builder.Services.AddDefaultIdentity<MyMVCMappingDEMOUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MyMVCMappingDEMOContext>();



builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();

//app.MapControllerRoute(
//    name: "login",
//    pattern: "",
//    defaults: new { controller = "Account", action = "Login", area = "Identity" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=MechEmp}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();
//app.MapGet("/", context =>
//{
//    context.Response.Redirect("/Identity/Account/Login");
//    return Task.CompletedTask;
//});
app.Run();
