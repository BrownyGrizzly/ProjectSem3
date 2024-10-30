using OnlineRailroad.Models;

namespace OnlineRailroad.Services
{
    public interface IStationService
    {
        Task<List<Station>> GetAllStationsAsync();
        Task<Station> GetStationByIdAsync(int id);
        Task AddStationAsync(Station station);
        Task UpdateStationAsync(Station station);
        Task DeleteStationAsync(int id);
    }

}
