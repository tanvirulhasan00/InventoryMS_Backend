using InventoryMS.Database.Data;
using InventoryMS.Models.Entities.ApplicationUserModel;
using InventoryMS.Models.Utilities;
using InventoryMS.Services.IServiceModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InventoryMS.Services.ServiceModels
{
    public class DbInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, InventoryMSDbContext context) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            await ApplyMigrationsAsync();
            await SeedRolesAsync();
            await SeedAdminUserAsync();
        }
        private async Task ApplyMigrationsAsync()
        {
            if ((await context.Database.GetPendingMigrationsAsync()).Any())
            {
                await context.Database.MigrateAsync();
            }
            else
            {
                Console.WriteLine("ℹ️ No pending migrations.");
            }
        }
        private async Task SeedRolesAsync()
        {
            string[] roles = { Roles.ADMIN, Roles.MANAGER, Roles.HOUSEMANAGER };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private async Task SeedAdminUserAsync()
        {
            const string adminEmail = "admin@gmail.com";
            const string adminPassword = "aDmin@00#";

            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

            if (existingAdmin == null)
            {
                var adminUser = new ApplicationUser
                {
                    FullName = "admin",
                    UserName = "admin",
                    Email = adminEmail,
                    PhoneNumber = "01970806028",
                    Password = adminPassword,
                    IsActive = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, Roles.ADMIN);
                }
                else
                {
                    throw new InvalidOperationException(
                        $"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}
