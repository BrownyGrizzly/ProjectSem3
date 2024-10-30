using Microsoft.EntityFrameworkCore;
using OnlineRailroad.Data;
using OnlineRailroad.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineRailroad.Services
{
    public class PassengerDetailService : IPassengerDetailService
    {
        private readonly AppDbContext _context;

        public PassengerDetailService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PassengerDetail>> GetAllPassengerDetailsAsync(int userId)
        {
            return await _context.PassengerDetails
                                 .Include(pd => pd.User)
                                 .Where(pd => pd.UserID == userId)
                                 .ToListAsync();
        }

        public async Task<PassengerDetail> GetPassengerDetailByIdAsync(long id, int userId)
        {
            return await _context.PassengerDetails
                                 .Include(pd => pd.User)
                                 .FirstOrDefaultAsync(pd => pd.PNRNo == id && pd.UserID == userId);
        }

        public async Task<PassengerDetail> CreatePassengerDetailAsync(PassengerDetail passengerDetail)
        {
            _context.PassengerDetails.Add(passengerDetail);
            await _context.SaveChangesAsync();
            return passengerDetail;
        }

        public async Task<PassengerDetail> UpdatePassengerDetailAsync(PassengerDetail passengerDetail)
        {
            _context.PassengerDetails.Update(passengerDetail);
            await _context.SaveChangesAsync();
            return passengerDetail;
        }

        public async Task<bool> DeletePassengerDetailAsync(long id, int userId)
        {
            var passengerDetail = await GetPassengerDetailByIdAsync(id, userId);
            if (passengerDetail == null) return false;

            _context.PassengerDetails.Remove(passengerDetail);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
