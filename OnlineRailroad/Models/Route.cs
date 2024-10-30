namespace OnlineRailroad.Models
{
    public class Route
    {
        public int RouteID { get; set; }  // Primary Key
        public string RouteName { get; set; }

        public ICollection<Schedule>? Schedules { get; set; }
        public ICollection<FareRule>? FareRules { get; set; }
    }
}
