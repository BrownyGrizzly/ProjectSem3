using System.ComponentModel.DataAnnotations;

namespace OnlineRailroad.Models
{
    public class Station
    {
        [Key]
        public int StationID { get; set; }  // Primary Key

        [Required]
        [MaxLength(100)]
        // Full name of the station
        public string StationName { get; set; }

        [Required]
        [MaxLength(10)]
        // Unique station code (e.g., abbreviation)
        public string StationCode { get; set; }

        public ICollection<Schedule>? Schedules { get; set; }
    }
}
