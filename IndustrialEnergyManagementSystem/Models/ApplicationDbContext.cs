using System.Data.Entity;

namespace IndustrialEnergyManagementSystem.Models
{
    public class ApplicationDbContext : DbContext
    {
       
        public ApplicationDbContext() : base("IEMS_DB") { }

        public DbSet<Machine> Machines { get; set; }
        public DbSet<EnergyRecord> EnergyRecords { get; set; }
    }
}