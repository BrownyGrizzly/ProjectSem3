using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineRailroad.Models;
using OnlineRailroad.Services;

namespace OnlineRailroad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly IStationService _stationService;

        public StationController(IStationService stationService)
        {
            _stationService = stationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStations()
        {
            return Ok(await _stationService.GetAllStationsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStationById(int id)
        {
            var station = await _stationService.GetStationByIdAsync(id);
            if (station == null) return NotFound();
            return Ok(station);
        }

        [Authorize(Roles = "ROLE_ADMIN,ROLE_EMPLOYEE")]
        [HttpPost]
        public async Task<IActionResult> AddStation([FromBody] Station station)
        {
            try
            {
                await _stationService.AddStationAsync(station);
                return CreatedAtAction(nameof(GetStationById), new { id = station.StationID }, station);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "ROLE_ADMIN,ROLE_EMPLOYEE")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStation(int id, [FromBody] Station station)
        {
            if (id != station.StationID) return BadRequest();
            try
            {
                await _stationService.UpdateStationAsync(station);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "ROLE_ADMIN,ROLE_EMPLOYEE")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStation(int id)
        {
            try
            {
                await _stationService.DeleteStationAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
