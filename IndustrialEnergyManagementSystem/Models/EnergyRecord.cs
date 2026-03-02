using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndustrialEnergyManagementSystem.Models
{
    [Table("EnergyRecord")]   // ✅ ADD THIS LINE
    public class EnergyRecord
    {
        [Key]
        public int RecordId { get; set; }

        [Required]
        public int MachineId { get; set; }

        [Required]
        [Display(Name = "Run Hours")]
        public double RunHours { get; set; }

        [Required]
        [Display(Name = "Record Date & Time")]
        public DateTime RecordDate { get; set; }

        [NotMapped]
        [Display(Name = "Energy (kWh)")]
        public double EnergyConsumed { get; set; }

        [ForeignKey("MachineId")]
        public virtual Machine Machine { get; set; }

        public double CalculateEnergy(double powerKW)
        {
            return powerKW * RunHours;
        }
    }
}