using System;

namespace IndustrialEnergyManagementSystem.Models
{
    public class EnergyLogViewModel
    {
        public int RecordId { get; set; }
        public string MachineName { get; set; }
        public double RunHours { get; set; }
        public double EnergyConsumed { get; set; }
        public DateTime RecordDate { get; set; }
    }
}