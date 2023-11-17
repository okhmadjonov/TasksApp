using Microsoft.AspNetCore.Identity;
using System;
using TasksControllerApp.Models;
using TeamProjectMVC.Entity.Enums;

namespace TasksControllerApp.DataContext
{
    public class Seed
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync((ERole.ADMIN).ToString()))
                    await roleManager.CreateAsync(new IdentityRole((ERole.ADMIN).ToString()));

                if (!await roleManager.RoleExistsAsync((ERole.MANAGER).ToString()))
                    await roleManager.CreateAsync(new IdentityRole((ERole.MANAGER).ToString()));

                if (!await roleManager.RoleExistsAsync((ERole.USER).ToString()))
                    await roleManager.CreateAsync(new IdentityRole((ERole.USER).ToString()));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminEmail = "admin@gmail.com";
                string managerEmail = "manager@gmail.com";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new User()
                    {
                        UserName = "admin",
                        Email = adminEmail,
                        EmailConfirmed = true,

                    };
                    var newManagerUser = new User()
                    {
                        UserName = "manager",
                        Email = managerEmail,
                        EmailConfirmed = true,

                    };
                    await userManager.CreateAsync(newAdminUser, "Admin1@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, (ERole.ADMIN).ToString());
                    //   2

                    await userManager.CreateAsync(newManagerUser, "Manager1@1234?");
                    await userManager.AddToRoleAsync(newManagerUser, (ERole.MANAGER).ToString());


                }

                string appUserEmail = "user@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new User()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,

                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, (ERole.USER).ToString());
                }

            }
        }
    }
}
