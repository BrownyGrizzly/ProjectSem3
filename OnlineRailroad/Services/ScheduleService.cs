using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineRailroad.Data;
using OnlineRailroad.Models;

namespace OnlineRailroad.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ScheduleService> _logger;

        public ScheduleService(AppDbContext context, ILogger<ScheduleService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Schedule>> GetAllSchedulesAsync()
        {
            try
            {
                return await _context.Schedules
                    .Include(s => s.Train)
                    .Include(s => s.Route)
                    .Include(s => s.Station)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all schedules");
                throw; // Re-throw or handle appropriately
            }
        }

        public async Task<Schedule> GetScheduleByIdAsync(int id)
        {
            try
            {
                return await _context.Schedules
                    .Include(s => s.Train)
                    .Include(s => s.Route)
                    .Include(s => s.Station)
                    .FirstOrDefaultAsync(s => s.ScheduleID == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching schedule with ID {id}");
                throw; // Re-throw or handle appropriately
            }
        }

        public async Task<Schedule> CreateScheduleAsync(Schedule schedule)
        {
            if (schedule == null) throw new ArgumentNullException(nameof(schedule));

            try
            {
                _context.Schedules.Add(schedule);
                await _context.SaveChangesAsync();
                return schedule;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error creating schedule");
                throw; // Re-throw or handle appropriately
            }
        }

        public async Task<Schedule> UpdateScheduleAsync(Schedule schedule)
        {
            if (schedule == null) throw new ArgumentNullException(nameof(schedule));

            try
            {
                _context.Schedules.Update(schedule);
                await _context.SaveChangesAsync();
                return schedule;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, $"Concurrency error updating schedule with ID {schedule.ScheduleID}");
                throw; // Handle concurrency exceptions as needed
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error updating schedule");
                throw; // Re-throw or handle appropriately
            }
        }

        public async Task<bool> DeleteScheduleAsync(int id)
        {
            try
            {
                var schedule = await _context.Schedules.FindAsync(id);
                if (schedule == null) return false;

                _context.Schedules.Remove(schedule);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Error deleting schedule with ID {id}");
                throw; // Re-throw or handle appropriately
            }
        }
    }
}
