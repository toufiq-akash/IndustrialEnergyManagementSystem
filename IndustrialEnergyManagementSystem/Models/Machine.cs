using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndustrialEnergyManagementSystem.Models
{
    public class Machine
    {
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public double PowerRatingKW { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
    }
}