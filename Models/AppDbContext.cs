using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkHolder.Models{
    public class AppDbContext : IdentityDbContext<AppUser>{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){
        }
        public DbSet<Link> Links { get; set; }
        public static async Task CreateAdminAccount(IServiceProvider serviceProvider,
                                                    IConfiguration configuration){
            UserManager<AppUser> userManager = 
                serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> roleManager = 
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            
            String username = configuration["Data:AdminUser:Name"];
            String email = configuration["Data:AdminUser:Email"];
            String password = configuration["Data:AdminUser:Password"];
            String role = configuration["Data:AdminUser:Role"];

            if(await userManager.FindByNameAsync(username) == null){
                if(await roleManager.FindByNameAsync(role) == null){
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
                AppUser user = new AppUser {
                    UserName = username,
                    Email = email
                };

                IdentityResult result = await userManager.CreateAsync(user, password);
                if(result.Succeeded){
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}