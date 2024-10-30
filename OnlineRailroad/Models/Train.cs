using System.ComponentModel.DataAnnotations;

namespace OnlineRailroad.Models
{
    public class Train
    {
        [Key]
        [Required]
        [MaxLength(20)]
        // Unique Train Number (Primary Key)
        public string TrainNo { get; set; }

        [Required]
        [MaxLength(100)]
        // Train name
        public string TrainName { get; set; }

        // Compartment Breakdown by Class
        [Range(0, int.MaxValue)]
        public int AC1Seats { get; set; } = 0;

        [Range(0, int.MaxValue)]
        public int AC2Seats { get; set; } = 0;

        [Range(0, int.MaxValue)]
        public int AC3Seats { get; set; } = 0;

        [Range(0, int.MaxValue)]
        public int SleeperSeats { get; set; } = 0;

        [Range(0, int.MaxValue)]
        public int GeneralSeats { get; set; } = 0;

        public ICollection<Schedule>? Schedules { get; set; }
        public ICollection<PassengerDetail>? PassengerDetails { get; set; }
    }
}
