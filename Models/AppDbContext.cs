using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LinkHolder.Models{
    public class AppDbContext : IdentityDbContext<AppUser>{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){
        }
        public DbSet<Link> Links { get; set; }
    }
}