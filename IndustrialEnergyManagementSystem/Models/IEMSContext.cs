using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IndustrialEnergyManagementSystem.Models
{
        public class IEMSContext : DbContext
        {
            public IEMSContext() : base("name=IEMS_DB")
            {
            }

            public DbSet<Machine> Machines { get; set; }
            public DbSet<Department> Departments { get; set; }
        }
}