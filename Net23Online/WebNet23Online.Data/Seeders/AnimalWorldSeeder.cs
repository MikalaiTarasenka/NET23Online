using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebNet23Online.Data.Enums;
using WebNet23Online.Data.Models;

namespace WebNet23Online.Data.Seeders
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(WebContext context)
        {
            if (await context.Users.AnyAsync())
            {
                return;
            }
                
            var admin = new UserData
            {
                Name = "admin",
                Password = "odmi",
                Role = UserRole.Admin,
                Language = Language.Russian,
                FirstName = "Админ",
                LastName = "Главный",
                Mobilephone = "+1234567890"
            };
            context.Users.Add(admin);
            await context.SaveChangesAsync();
        }
    }
}
