using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MzaEventos.Models;

namespace MzaEventos.Data
{
    public class DbSeeder
    {
        public static async Task Seed(EventosDbContext context, UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            //context.Database.EnsureCreated();
            await context.Database.MigrateAsync();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var adminUser = await userManager.FindByEmailAsync("admin@admin.com");
            if (adminUser == null)
            {
                adminUser = new Usuario
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    Nombre = "Administrador",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }

            }

            //context.SaveChanges();
        }
    }
}
