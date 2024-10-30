using Microsoft.EntityFrameworkCore;
using OnlineRailroad.Data;
using OnlineRailroad.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRailroad.Services
{
    public class TrainService : ITrainService
    {
        private readonly AppDbContext _context;

        public TrainService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Train>> GetAllTrainsAsync()
        {
            return await _context.Trains.ToListAsync();
        }

        public async Task<Train> GetTrainByNoAsync(string trainNo)
        {
            return await _context.Trains.FirstOrDefaultAsync(t => t.TrainNo == trainNo);
        }

        public async Task<IEnumerable<Train>> GetTrainsByNameAsync(string trainName)
        {
            return await _context.Trains
                                 .Where(t => t.TrainName.Contains(trainName))
                                 .ToListAsync();
        }

        public async Task AddTrainAsync(Train train)
        {
            await _context.Trains.AddAsync(train);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTrainAsync(string trainNo, Train train)
        {
            var existingTrain = await _context.Trains.FirstOrDefaultAsync(t => t.TrainNo == trainNo);
            if (existingTrain != null)
            {
                existingTrain.TrainName = train.TrainName;
                existingTrain.AC1Seats = train.AC1Seats;
                existingTrain.AC2Seats = train.AC2Seats;
                existingTrain.AC3Seats = train.AC3Seats;
                existingTrain.SleeperSeats = train.SleeperSeats;
                existingTrain.GeneralSeats = train.GeneralSeats;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteTrainAsync(string trainNo)
        {
            var train = await _context.Trains.FirstOrDefaultAsync(t => t.TrainNo == trainNo);
            if (train != null)
            {
                _context.Trains.Remove(train);
                await _context.SaveChangesAsync();
            }
        }
    }
}
