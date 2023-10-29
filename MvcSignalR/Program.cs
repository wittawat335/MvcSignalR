using Microsoft.EntityFrameworkCore;
using MvcSignalR.Entities;
using MvcSignalR.Hubs;
using MvcSignalR.MiddlewareExtensions;
using MvcSignalR.Repositories;
using MvcSignalR.SubscribeTableDependencies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

// connectionString
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SignalRnotiContext>(options =>
    options.UseSqlServer(connectionString),
    ServiceLifetime.Singleton);
builder.Services.AddDatabaseDeveloperPageExceptionFilter(); //install Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore

// DI
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<NotificationHub>();
builder.Services.AddSingleton<SubscribeTableDependency>();

// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapHub<NotificationHub>("/notificationHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=SignIn}/{id?}");
app.UseSqlTableDependency<SubscribeTableDependency>(connectionString);
app.MapRazorPages();
app.Run();
