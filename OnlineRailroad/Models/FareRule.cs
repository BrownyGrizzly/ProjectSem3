using OnlineRailroad.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineRailroad.Models
{
    public class FareRule
    {
        [Key]
        public int FareRuleID { get; set; } // Primary Key

        [Required]
        [ForeignKey("Route")] // Explicitly link this property to the Route navigation property
        public int RouteID { get; set; } // Foreign key linking to Route

        [Required]
        public TravelClass Class { get; set; } // Enum for travel class

        [Required]
        public decimal BasePricePerKm { get; set; } // Base price per kilometer

        // Navigation property for Route
        public Route Route { get; set; }
    }
}
