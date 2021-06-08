using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.Entities;

namespace test
{
    public class IdentityInitializier
    {
        public static void CreateAdmin(UserManager<AppUser> userManager , RoleManager<IdentityRole> roleManager)
        {
            AppUser appUser = new AppUser
            {
                Name = "Ezgi",
                SurName="Polat",
                UserName="Ezgi"
            };
            if (userManager.FindByNameAsync("Ezgi").Result == null)
            {
                var identityResult = userManager.CreateAsync(appUser,"1").Result;
            }
            if (roleManager.FindByNameAsync("Admin").Result == null)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin"
                };
                var identityResult = roleManager.CreateAsync(role).Result;
                var result= userManager.AddToRoleAsync(appUser, role.Name).Result;
            }
        }
    }
}
