using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FinalMVC.Areas.Identity.Data;
using MVCWithDapper.DBContext;
using FinalMVC.Services;
using Microsoft.AspNet.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DapperContextConnection") ?? throw new InvalidOperationException("Connection string 'DapperContextConnection' not found.");

builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<ITransactionService, TransactionService>();
builder.Services.AddSingleton<IWalletService, WalletService>();



builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<DBContext>();

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
app.UseAuthentication();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "insertTransaction",
        pattern: "User/InsertTransaction",
        defaults: new { controller = "User", action = "InsertTransaction" });

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.MapRazorPages();

app.Run();
