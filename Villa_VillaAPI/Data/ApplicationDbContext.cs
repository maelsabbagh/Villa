using Microsoft.EntityFrameworkCore;
using Villa_VillaAPI.Models;

namespace Villa_VillaAPI.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Villa> Villas { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }
    }
}
