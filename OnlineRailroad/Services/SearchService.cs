using Microsoft.EntityFrameworkCore;
using OnlineRailroad.Data;
using OnlineRailroad.Models;

namespace OnlineRailroad.Services
{
    public class SearchService : ISearchService
    {
        private readonly AppDbContext _context;

        public SearchService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Train> SearchTrains(int sourceStationId, int destinationStationId, DateTime travelDate)
        {
            // Validate input parameters
            if (sourceStationId <= 0 || destinationStationId <= 0 || sourceStationId == destinationStationId)
                throw new ArgumentException("Invalid source or destination station ID.");

            // Fetch schedules for the specified source and destination stations on the given travel date
            var schedules = _context.Schedules
                .Include(s => s.Train)
                .Where(s => (s.StationID == sourceStationId || s.StationID == destinationStationId)
                             && s.ArrivalTime.Date == travelDate.Date)
                .ToList();

            // Group schedules by TrainID to find valid trains
            var validTrains = schedules
                .GroupBy(s => s.TrainNo)
                .Where(g =>
                    g.Any(s => s.StationID == sourceStationId) &&
                    g.Any(s => s.StationID == destinationStationId))
                .Select(g => g.First().Train)
                .ToList();

            if (!validTrains.Any())
            {
                throw new Exception("No trains found for the selected route and date.");
            }

            return validTrains;
        }

        public decimal CalculateTotalDistance(int sourceStationId, int destinationStationId, string trainId, DateTime travelDate)
        {
            // Fetch schedules for the specified train and travel date
            var schedules = _context.Schedules
                .Include(s => s.Route) // Include Route if necessary for further logic
                .Where(s => s.TrainNo == trainId && s.ArrivalTime.Date == travelDate.Date)
                .ToList();

            // Ensure we have a valid schedule for source and destination
            var sourceSchedule = schedules.FirstOrDefault(s => s.StationID == sourceStationId);
            var destinationSchedule = schedules.FirstOrDefault(s => s.StationID == destinationStationId);

            if (sourceSchedule == null || destinationSchedule == null)
                throw new Exception("Train does not stop at one of the selected stations.");

            // Calculate the total distance
            decimal totalDistance = 0;
            var stationOrder = Math.Min(sourceSchedule.StationOrder, destinationSchedule.StationOrder);
            var endStationOrder = Math.Max(sourceSchedule.StationOrder, destinationSchedule.StationOrder);

            // Iterate over the schedules in the range between source and destination
            for (int i = stationOrder; i < endStationOrder; i++)
            {
                var currentSchedule = schedules.FirstOrDefault(s => s.StationOrder == i);
                var nextSchedule = schedules.FirstOrDefault(s => s.StationOrder == i + 1);

                if (currentSchedule != null && nextSchedule != null)
                {
                    var distance = _context.Distances
                        .FirstOrDefault(d => (d.StationAId == currentSchedule.StationID && d.StationBId == nextSchedule.StationID) ||
                                             (d.StationAId == nextSchedule.StationID && d.StationBId == currentSchedule.StationID));

                    if (distance != null)
                    {
                        totalDistance += distance.DistanceKm;
                    }
                }
            }

            return totalDistance;
        }

        public async Task<List<int>> GetAvailableSeatsAsync(string trainNo, DateTime travelDate)
        {
            // Get all booked tickets for the train on the specified date
            var bookedTickets = await _context.PassengerDetails
                .Where(t => t.TrainNo == trainNo && t.DateOfTravel.Date == travelDate.Date)
                .ToListAsync();

            // Get total seats from the Train
            var train = await _context.Trains.FirstOrDefaultAsync(t => t.TrainNo == trainNo);
            if (train == null)
                throw new Exception("Train not found.");

            // Initialize all available seats
            var allSeats = Enumerable.Range(1, train.AC1Seats + train.AC2Seats + train.AC3Seats + train.SleeperSeats + train.GeneralSeats).ToList();

            // Extract booked seat numbers
            var bookedSeatNumbers = bookedTickets.Select(t => t.SeatNumber).ToList();

            // Calculate available seats
            var availableSeats = allSeats.Except(bookedSeatNumbers).ToList();

            return availableSeats;
        }
    }
}
