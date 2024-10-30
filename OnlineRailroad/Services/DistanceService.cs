using Microsoft.EntityFrameworkCore;
using OnlineRailroad.Data;
using OnlineRailroad.Models;

namespace OnlineRailroad.Services
{
    public class DistanceService : IDistanceService
    {
        private readonly AppDbContext _context;

        public DistanceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Distance>> GetAllDistancesAsync()
        {
            return await _context.Distances.ToListAsync();
        }

        public async Task<Distance> GetDistanceByIdAsync(int id)
        {
            return await _context.Distances.FindAsync(id);
        }

        public async Task AddDistanceAsync(Distance distance)
        {
            // Validate the distance object
            if (distance == null) throw new ArgumentNullException(nameof(distance));
            // Add any additional validation as needed

            _context.Distances.Add(distance);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDistanceAsync(Distance distance)
        {
            // Validate the distance object
            if (distance == null) throw new ArgumentNullException(nameof(distance));
            if (!await DistanceExists(distance.DistanceID)) throw new KeyNotFoundException("Distance not found.");

            _context.Distances.Update(distance);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDistanceAsync(int id)
        {
            var distance = await _context.Distances.FindAsync(id);
            if (distance != null)
            {
                _context.Distances.Remove(distance);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Distance not found.");
            }
        }

        private async Task<bool> DistanceExists(int id)
        {
            return await _context.Distances.AnyAsync(e => e.DistanceID == id);
        }
    }
}
