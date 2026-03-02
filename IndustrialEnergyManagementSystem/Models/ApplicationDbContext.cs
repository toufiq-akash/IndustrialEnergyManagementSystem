using System.Data.Entity;

namespace IndustrialEnergyManagementSystem.Models
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor: name of connection string in Web.config
        public ApplicationDbContext() : base("IEMS_DB") { }

        // Tables
        public DbSet<Machine> Machines { get; set; }
        public DbSet<EnergyRecord> EnergyRecords { get; set; }
    }
}