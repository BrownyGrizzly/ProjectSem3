using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineRailroad.Models
{
    public class Distance
    {
        [Key]
        public int DistanceID { get; set; } // Primary Key

        [Required]
        [ForeignKey("StationA")] // Explicitly link to StationA navigation property
        public int StationAId { get; set; }  // Foreign Key referencing Station

        [Required]
        [ForeignKey("StationB")] // Explicitly link to StationB navigation property
        public int StationBId { get; set; }  // Foreign Key referencing Station

        [Required]
        public decimal DistanceKm { get; set; }  // Distance in kilometers

        // Navigation properties for associated stations
        public virtual Station StationA { get; set; }
        public virtual Station StationB { get; set; }
    }
}
