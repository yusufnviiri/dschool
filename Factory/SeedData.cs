using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;



namespace victors.Factory
{
  

    
        public static class SeedData
        {
            public static async Task Initialize(IServiceProvider serviceProvider)
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                // Seed roles
                string[] roleNames = { "manager", "Director" };
                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                // Seed users
                var adminUser = await userManager.FindByEmailAsync("director@victors");
                if (adminUser == null)
                {
                    var user = new IdentityUser
                    {
                        UserName = "director",
                        Email = "director@victors",
                        
                    };

                    var result = await userManager.CreateAsync(user, "Victors@1");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Director");
                    }
                }
            }
        }
    }


