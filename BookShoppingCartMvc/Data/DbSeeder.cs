// Data/DbInitializer.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BookShoppingCartMvc.Data
{
    public static class DbSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider provider)
        {
            // Get UserManager and RoleManager correctly
            var userManager = provider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

            //  Seed Roles from Enum
            foreach (var roleName in Enum.GetNames(typeof(Roles)))
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //  Seed Admin User
            var adminUsername = "Mohamed Samir";
            var adminEmail = "mohamedsamir@gmail.com";
            var adminPassword = "qweasd123";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminUsername,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, Roles.Admin.ToString());
                }
            }

            //  Seed Normal User
            var userUsername = "baha";
            var userEmail = "baha@example.com";
            var userPassword = "User123";

            var normalUser = await userManager.FindByEmailAsync(userEmail);
            if (normalUser == null)
            {
                normalUser = new IdentityUser
                {
                    UserName = userUsername,
                    Email = userEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(normalUser, userPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(normalUser, Roles.User.ToString());
                }
            }
        }
    }
}
