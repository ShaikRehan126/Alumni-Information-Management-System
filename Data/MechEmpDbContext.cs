using Microsoft.EntityFrameworkCore;
using MyMVCMappingDEMO.Models;
namespace MyMVCMappingDEMO.Data
{
    public class MechEmpDbContext : DbContext
    {
        public MechEmpDbContext(DbContextOptions<MechEmpDbContext> options)
            : base(options)
        {
        }

        public DbSet<MechEmployee>MechEmployees { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}
