using LearnToCode.Data;
using LearnToCode.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearnToCode.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<User> User { get; set; }
    }
}
