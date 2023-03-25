using Microsoft.EntityFrameworkCore;

namespace Demo.Models.Domain
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }
        public DbSet<Student> Students { get; set; }
    }
}
