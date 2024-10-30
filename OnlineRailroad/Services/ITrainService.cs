using Microsoft.AspNetCore.Mvc;
using OnlineRailroad.Models;

namespace OnlineRailroad.Services
{
    public interface ITrainService
    {
        Task<IEnumerable<Train>> GetAllTrainsAsync();
        Task<Train> GetTrainByNoAsync(string trainNo);
        Task AddTrainAsync(Train train);
        Task UpdateTrainAsync(string trainNo, Train train);
        Task DeleteTrainAsync(string trainNo);
        Task<IEnumerable<Train>> GetTrainsByNameAsync(string trainName);
    }
}
