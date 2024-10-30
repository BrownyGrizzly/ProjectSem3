using OnlineRailroad.Models;

namespace OnlineRailroad.Services
{
    public interface IDistanceService
    {
        Task<List<Distance>> GetAllDistancesAsync();
        Task<Distance> GetDistanceByIdAsync(int id);
        Task AddDistanceAsync(Distance distance);
        Task UpdateDistanceAsync(Distance distance);
        Task DeleteDistanceAsync(int id);
    }
}
