using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineRailroad.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleID { get; set; }  // Primary Key

        [Required]
        [ForeignKey("Train")]
        public string TrainNo { get; set; }  // Foreign Key referencing Train's primary key
        public Train Train { get; set; }

        [Required]
        [ForeignKey("Route")]
        public int RouteID { get; set; }  // Foreign Key referencing Route's primary key
        public Route Route { get; set; }

        [Required]
        [ForeignKey("Station")]
        public int StationID { get; set; }  // Foreign Key referencing Station's primary key
        public Station Station { get; set; }

        [Required]
        public int StationOrder { get; set; }  // Order of the station in this route

        [Required]
        public DateTime ArrivalTime { get; set; }  // Train arrival time at this station

        [Required]
        public DateTime DepartureTime { get; set; }  // Train departure time at this station
    }
}
