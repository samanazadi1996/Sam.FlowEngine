using FlowEngine.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FlowEngine.Infrastructure.Identity.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "Admin",
                Email = "Admin@Admin.com",
                Name = "Saman",
                PhoneNumber = "09304241296",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (!await userManager.Users.AnyAsync(p => p.UserName == defaultUser.UserName))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Sam@12345");
                }
            }

            var defaultUser2 = new ApplicationUser
            {
                UserName = "Test",
                Email = "Test@Admin.com",
                Name = "Test",
                PhoneNumber = "09123654789",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (!await userManager.Users.AnyAsync(p => p.UserName == defaultUser2.UserName))
            {
                var user = await userManager.FindByEmailAsync(defaultUser2.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser2, "Test@12345");
                }
            }
        }
    }
}
