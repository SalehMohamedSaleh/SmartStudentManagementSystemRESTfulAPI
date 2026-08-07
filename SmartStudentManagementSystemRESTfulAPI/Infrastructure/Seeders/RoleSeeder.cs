using Microsoft.AspNetCore.Identity;

namespace SmartStudentManagementSystemRESTfulAPI.Infrastructure.Seeders
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<IdentityRole<int>> roleManager)
        {
            string[] roles =
            {
                "Admin",
                "Teacher",
                "Student"
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(
                        new IdentityRole<int>
                        {
                            Name = role
                        });
                }
            }
        }
    }
}