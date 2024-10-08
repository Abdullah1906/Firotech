using Microsoft.AspNetCore.Identity;

namespace Firotechbd.Services
{
    public class UserSeedingService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserSeedingService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedDefaultAdminUserAsync()
        {
            var adminEmail = "admin@firotechbd.com";
            var adminPassword = "Firotechbd223@!";

            try
            {
                // Check if the Admin role exists, if not, create it
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                // Check if the admin user already exists
                var adminUser = await _userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    // Create the admin user
                    adminUser = new IdentityUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true // Automatically confirm email for the admin
                    };
                    var result = await _userManager.CreateAsync(adminUser, adminPassword);

                    if (result.Succeeded)
                    {
                        // Assign the Admin role to the user
                        await _userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                    else
                    {
                        // Log any errors during user creation
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($"Error: {error.Description}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception if something goes wrong
                Console.WriteLine($"Exception occurred: {ex.Message}");
            }
        }
    }
}
