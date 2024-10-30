using Microsoft.AspNetCore.Mvc;
using OnlineRailroad.Models;
using OnlineRailroad.Services;

namespace OnlineRailroad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistanceController : ControllerBase
    {
        private readonly IDistanceService _distanceService;

        public DistanceController(IDistanceService distanceService)
        {
            _distanceService = distanceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDistances()
        {
            var distances = await _distanceService.GetAllDistancesAsync();
            return Ok(distances);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDistanceById(int id)
        {
            var distance = await _distanceService.GetDistanceByIdAsync(id);
            if (distance == null) return NotFound(new { Message = "Distance not found." });
            return Ok(distance);
        }

        [HttpPost]
        public async Task<IActionResult> AddDistance([FromBody] Distance distance)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _distanceService.AddDistanceAsync(distance);
                return CreatedAtAction(nameof(GetDistanceById), new { id = distance.DistanceID }, distance);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while adding the distance." });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDistance(int id, [FromBody] Distance distance)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != distance.DistanceID) return BadRequest(new { Message = "Distance ID mismatch." });

            try
            {
                await _distanceService.UpdateDistanceAsync(distance);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the distance." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistance(int id)
        {
            try
            {
                await _distanceService.DeleteDistanceAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the distance." });
            }
        }
    }
}
