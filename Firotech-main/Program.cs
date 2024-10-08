using Firotechbd.Data;
using Firotechbd.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("FirotechbdContextConnection");
builder.Services.AddDbContext<FirotechbdContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<FirotechbdContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<UserSeedingService>();
builder.Services.AddScoped<FileUploadHelper>();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var userSeedingService = services.GetRequiredService<UserSeedingService>();

    try
    {
        await userSeedingService.SeedDefaultAdminUserAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while seeding the admin user: {ex.Message}");
    }
}
app.UseMiddleware<Firotechbd.Services.LinkBlockerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

// Default route configuration
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
            name: "employeeProfile",
            pattern: "{slug}",
            defaults: new { controller = "Employee", action = "ViewProfile" });
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();  
});
app.MapRazorPages(); // If using Razor Pages

app.Run();
