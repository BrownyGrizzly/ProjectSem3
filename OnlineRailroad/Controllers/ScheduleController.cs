using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineRailroad.Models;
using OnlineRailroad.Services;

namespace OnlineRailroad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetAllSchedules()
        {
            var schedules = await _scheduleService.GetAllSchedulesAsync();
            return Ok(schedules);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetScheduleById(int id)
        {
            var schedule = await _scheduleService.GetScheduleByIdAsync(id);
            if (schedule == null) return NotFound();
            return Ok(schedule);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")] // Only Admin and Employee can create schedules
        public async Task<ActionResult<Schedule>> CreateSchedule([FromBody] Schedule schedule)
        {
            if (schedule == null) return BadRequest("Schedule cannot be null");

            var createdSchedule = await _scheduleService.CreateScheduleAsync(schedule);
            return CreatedAtAction(nameof(GetScheduleById), new { id = createdSchedule.ScheduleID }, createdSchedule);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Employee")] // Only Admin and Employee can update schedules
        public async Task<ActionResult<Schedule>> UpdateSchedule(int id, [FromBody] Schedule schedule)
        {
            if (schedule == null) return BadRequest("Schedule cannot be null");
            if (id != schedule.ScheduleID) return BadRequest("ID mismatch");

            var updatedSchedule = await _scheduleService.UpdateScheduleAsync(schedule);
            return Ok(updatedSchedule);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Employee")] // Only Admin and Employee can delete schedules
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var result = await _scheduleService.DeleteScheduleAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
