using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;
using System.Collections.Generic;

namespace MIT.CRM.Models
{
    public class SampleData
    {
        public static async Task InitializeIdentityDatabaseAsync(IServiceProvider serviceProvider)
        {
            using (var db = serviceProvider.GetRequiredService<ApplicationDbContext>())
            {
                //if (await db.Database.EnsureCreatedAsync())
                //{
                    await CreateAdminUser(serviceProvider);
                //}
            }
        }

        /// <summary>
        /// Creates a store manager user who can manage the inventory.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            //var options = serviceProvider.GetRequiredService<IOptions<IdentityDbContextOptions>>().Value;
            const string adminRole = "Administrator";

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var list = new List<string>() { "Administrator","Cobranças","Director de Area","Funcionario" };
            

            foreach(var role in list)
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var user = await userManager.FindByNameAsync("gmahota@mit.co.mz");

            if (user == null)
            {
                user = new ApplicationUser { UserName = "gmahota@mit.co.mz" };
                await userManager.CreateAsync(user, "Accsys2011!");
                await userManager.AddToRoleAsync(user, adminRole);
                await userManager.AddClaimAsync(user, new Claim("ManageStore", "Allowed"));
            }
            else
            {
                //await userManager.AddToRoleAsync(user, adminRole);
                //await userManager.AddClaimAsync(user, new Claim("ManageStore", "Allowed"));
            }
        }
    }
}
