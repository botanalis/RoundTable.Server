using Microsoft.EntityFrameworkCore;
using RoundTable.Server.Data.Model;

namespace RoundTable.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }
        
    }
}