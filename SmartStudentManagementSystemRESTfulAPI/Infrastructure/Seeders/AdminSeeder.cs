using Microsoft.AspNetCore.Identity;

namespace SmartStudentManagementSystemRESTfulAPI.Infrastructure.Seeders
{
    public static class AdminSeeder
    {
        public static async Task SeedAdminAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<int>> roleManager)
        {
            // 1. تأكد من وجود دور Admin
            var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
            if (!adminRoleExists)
            {
                await roleManager.CreateAsync(new IdentityRole<int> { Name = "Admin" });
            }

            // 2. تحقق من وجود Admin موجود بالفعل
            var adminUser = await userManager.FindByEmailAsync("admin@school.com");
            if (adminUser != null)
                return; // Admin موجود بالفعل

            // 3. إنشاء Admin الأول
            var newAdmin = new ApplicationUser
            {
                UserName = "admin@school.com",
                Email = "admin@school.com",
                FirstName = "System",
                LastName = "Administrator",
            };

            var result = await userManager.CreateAsync(newAdmin, "Admin@123");

            if (result.Succeeded)
            {
                // 4. إضافة دور Admin
                await userManager.AddToRoleAsync(newAdmin, "Admin");
            }
        }
    }
}