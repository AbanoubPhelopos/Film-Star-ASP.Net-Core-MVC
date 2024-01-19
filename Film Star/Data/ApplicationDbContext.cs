using Film_Star.Models;
using Microsoft.EntityFrameworkCore;
namespace Film_Star.Data
{
	public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
                
        }
              
        public DbSet<Film> films { get; set; }  
    }
}
