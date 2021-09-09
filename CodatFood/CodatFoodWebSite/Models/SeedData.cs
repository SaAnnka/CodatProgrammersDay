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
                        Name = "Vegetables"
                    },
                    new Category
                    {
                        Name = "Berries"
                    }
                );

                await context.SaveChangesAsync();

                context.Plant.AddRange(
                    new Plant
                    {
                        Name = "Cucumber",
                        Description = "Cucumbers are very green",
                        CategoryId = context.Category.Single(c => c.Name == "Vegetables").Id
                    },
                    new Plant
                    {
                        Name = "Tomato",
                        Description = "Tomatoes are very red",
                        CategoryId = context.Category.Single(c => c.Name == "Vegetables").Id
                    },
                    new Plant
                    {
                        Name = "Blueberries",
                        Description = "Blueberries are very blue",
                        CategoryId = context.Category.Single(c => c.Name == "Berries").Id
                    }
                );

                await context.SaveChangesAsync();

                await CreateRoles(serviceProvider);
            }
        }

        private static async Task CreateRoles(IServiceProvider serviceProvider)
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
