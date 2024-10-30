﻿using OnlineRailroad.Models;

namespace OnlineRailroad.Services
{
    public interface IScheduleService
    {
        Task<IEnumerable<Schedule>> GetAllSchedulesAsync();
        Task<Schedule> GetScheduleByIdAsync(int id);
        Task<Schedule> CreateScheduleAsync(Schedule schedule);
        Task<Schedule> UpdateScheduleAsync(Schedule schedule);
        Task<bool> DeleteScheduleAsync(int id);
    }
}
