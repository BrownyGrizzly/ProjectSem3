using OnlineRailroad.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineRailroad.Services
{
    public interface IPassengerDetailService
    {
        Task<IEnumerable<PassengerDetail>> GetAllPassengerDetailsAsync(int userId);
        Task<PassengerDetail> GetPassengerDetailByIdAsync(long id, int userId);
        Task<PassengerDetail> CreatePassengerDetailAsync(PassengerDetail passengerDetail);
        Task<PassengerDetail> UpdatePassengerDetailAsync(PassengerDetail passengerDetail);
        Task<bool> DeletePassengerDetailAsync(long id, int userId);
    }
}
