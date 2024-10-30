using OnlineRailroad.Models;

namespace OnlineRailroad.Services
{
    public interface ISearchService
    {
        IEnumerable<Train> SearchTrains(int sourceStationId, int destinationStationId, DateTime travelDate);
        decimal CalculateTotalDistance(int sourceStationId, int destinationStationId, string trainId, DateTime travelDate);
        Task<List<int>> GetAvailableSeatsAsync(string trainNo, DateTime travelDate);
    }
}
