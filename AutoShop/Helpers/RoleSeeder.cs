using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace AutoShop.Helpers
{
    public enum Roles
    {
        User,
        Admin,
    }

    public static class RoleSeeder
    {
        public static async Task SeedRoles(this IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (string role in Enum.GetNames(typeof(Roles)))
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task SeedAdmin(this IServiceProvider serviceProvider)
        {
            UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            const string USERNAME = "admin@admin.com";
            const string PASSWORD = "Admin-1";

            User user = new User()
            {
                UserName = USERNAME,
                Email = USERNAME,
                EmailConfirmed = true,
            };

            var res = userManager.CreateAsync(user, PASSWORD).Result;

            if (res.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }
}