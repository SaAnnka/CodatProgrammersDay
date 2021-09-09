using System;
using System.Linq;
using System.Threading.Tasks;
using CodatFoodWebSite.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CodatFoodWebSite.Models
{
    public static class SeedData
    {
        private const string AdminEmail = "admin@codat.io";
        private const string AdminPassword = "Codat12345!";

        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                if (context.Plant.Any())
                {
                    return;   // DB has been seeded
                }

                context.Category.AddRange(
                    new Category
                    {
                        Id = 1,
                        Name = "Vegetables"
                    },
                    new Category
                    {
                        Id = 2,
                        Name = "Berries"
                    }
                );

                context.Plant.AddRange(
                    new Plant
                    {
                        Id = 1,
                        Name = "Cucumber",
                        Description = "Cucumbers are very green",
                        CategoryId = 1
                    },
                    new Plant
                    {
                        Id = 2,
                        Name = "Tomato",
                        Description = "Tomatoes are very red",
                        CategoryId = 1
                    },
                    new Plant
                    {
                        Id = 3,
                        Name = "Blueberries",
                        Description = "Blueberries are very blue",
                        CategoryId = 1
                    }
                );

                context.SaveChanges();

                await CreateRoles(serviceProvider);
            }
        }

        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "admin" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var powerUser = new IdentityUser
            {
                UserName = AdminEmail,
                Email = AdminEmail,
                EmailConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(AdminEmail);

            if (user == null)
            {
                var createPowerUser = await userManager.CreateAsync(powerUser, AdminPassword);
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(powerUser, "admin");
                }
            }
        }
    }
}
