using BCrypt.Net;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;




namespace DAL.Data
{
    public static class ContextSeed
    {
        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

                // Check if the admin user already exists
                var adminUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == "admin@gmail.com");
                if (adminUser == null)
                {


                    var admin = new User
                    {
                        FullName = "Admin",
                        Email = "admin@gmail.com",
                        PhoneNumber = 1234567890,
                        Role = "Admin" // Assign the role based on IsAdmin property
                    };
                    var regularUser = new User
                    {
                        FullName = "Utkarsh Bishnoi",
                        Email = "user@gmail.com",
                        PhoneNumber = 1234567890,
                        Role = "User" // Assign the role based on IsAdmin property
                    };
                    // Generate the password hash
                    regularUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123#");
                    admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123#");

                    // Add the admin user to the database
                    await dbContext.Users.AddAsync(regularUser);
                    await dbContext.Users.AddAsync(admin);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}

