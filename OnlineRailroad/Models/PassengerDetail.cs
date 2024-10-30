using OnlineRailroad.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineRailroad.Models
{
    public class PassengerDetail
    {
        [Key]
        public long PNRNo { get; set; } // Unique PNR number for each passenger

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } // Name of the passenger

        [Required]
        public int Age { get; set; } // Age of the passenger

        [Required]
        public Gender Gender { get; set; } // Gender (using enum)

        [Required]
        public TravelClass Class { get; set; } // Class of travel (using enum)

        [Required]
        [ForeignKey("Train")] // Set TrainNo as a foreign key
        public string TrainNo { get; set; } // Train number


        [Required]
        public DateTime DateOfTravel { get; set; } // Date of travel

        [Required]
        public int SeatNumber { get; set; } // Assigned seat number

        [Required]
        public decimal Price { get; set; } // Price of the ticket

        [Required]
        public PaymentStatus PaymentStatus { get; set; } // Payment status (Pending, Paid, etc.)

        [ForeignKey("User")]
        public int UserID { get; set; } // Foreign key for User

        // Navigation property for the associated User
        public User User { get; set; }

        public Train Train { get; set; }
    }
}
