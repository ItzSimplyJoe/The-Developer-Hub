using DeveloperHub.Data;
using DeveloperHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeveloperHub.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<User> User { get; set; }
    }
}
