using Microsoft.EntityFrameworkCore;
using OnlineRailroad.Data;
using OnlineRailroad.Models;

namespace OnlineRailroad.Services
{
    public class StationService : IStationService
    {
        private readonly AppDbContext _context;

        public StationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Station>> GetAllStationsAsync()
        {
            return await _context.Stations.ToListAsync();
        }

        public async Task<Station> GetStationByIdAsync(int id)
        {
            return await _context.Stations.FindAsync(id);
        }

        public async Task AddStationAsync(Station station)
        {
            _context.Stations.Add(station);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStationAsync(Station station)
        {
            var existingStation = await _context.Stations.FindAsync(station.StationID);
            if (existingStation == null)
            {
                throw new KeyNotFoundException("Station not found.");
            }

            existingStation.StationName = station.StationName;
            existingStation.StationCode = station.StationCode;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteStationAsync(int id)
        {
            var station = await _context.Stations.FindAsync(id);
            if (station != null)
            {
                _context.Stations.Remove(station);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Station not found.");
            }
        }
    }
}
