using OnlineRailroad.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineRailroad.Models
{
    public class User
    {
        [Key]
        // Primary key for the user
        public int UserID { get; set; }

        [Required]
        [MaxLength(100)]
        // Username of the user (used for login)
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        // Email of the user (must be a valid email format)
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        // Hashed password for security purposes
        public string PasswordHash { get; set; }

        [MaxLength(150)]
        // Full name of the user (optional)
        public string? FullName { get; set; }

        [MaxLength(15)]
        // Phone number of the user (optional)
        public string? PhoneNumber { get; set; }

        [Required]
        // Type of the user (Admin, Employee, or Regular User)
        public UserType UserType { get; set; }

        [MaxLength(255)]
        // Address of the user (optional)
        public string? Address { get; set; }

        [DataType(DataType.Date)]
        // Date of birth of the user (optional)
        public DateTime? DateOfBirth { get; set; }

        // Date the user registered (default to current time)
        public DateTime RegisteredDate { get; set; } = DateTime.Now;

        [MaxLength(255)]
        // URL or path to the user's profile picture (optional)
        public string? ProfilePicture { get; set; }

        public ICollection<PassengerDetail>? PassengerDetails { get; set; }
    }
}
