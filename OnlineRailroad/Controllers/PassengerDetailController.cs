using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineRailroad.Models;
using OnlineRailroad.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineRailroad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerDetailController : ControllerBase
    {
        private readonly IPassengerDetailService _passengerDetailService;

        public PassengerDetailController(IPassengerDetailService passengerDetailService)
        {
            _passengerDetailService = passengerDetailService;
        }

        private bool TryGetUserId(out int userId)
        {
            // Try to parse the UserId from the claim as an integer
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(userIdClaim, out userId);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PassengerDetail>>> GetAllPassengerDetails()
        {
            if (!TryGetUserId(out int userId))
            {
                return Unauthorized("Invalid User ID.");
            }

            var passengerDetails = await _passengerDetailService.GetAllPassengerDetailsAsync(userId);
            return Ok(passengerDetails);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<PassengerDetail>> GetPassengerDetailById(long id)
        {
            if (!TryGetUserId(out int userId))
            {
                return Unauthorized("Invalid User ID.");
            }

            var passengerDetail = await _passengerDetailService.GetPassengerDetailByIdAsync(id, userId);
            if (passengerDetail == null) return NotFound();
            return Ok(passengerDetail);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PassengerDetail>> CreatePassengerDetail([FromBody] PassengerDetail passengerDetail)
        {
            if (!TryGetUserId(out int userId))
            {
                return Unauthorized("Invalid User ID.");
            }

            passengerDetail.UserID = userId;

            try
            {
                var createdPassengerDetail = await _passengerDetailService.CreatePassengerDetailAsync(passengerDetail);
                return CreatedAtAction(nameof(GetPassengerDetailById), new { id = createdPassengerDetail.PNRNo }, createdPassengerDetail);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<PassengerDetail>> UpdatePassengerDetail(long id, [FromBody] PassengerDetail passengerDetail)
        {
            if (!TryGetUserId(out int userId))
            {
                return Unauthorized("Invalid User ID.");
            }

            if (id != passengerDetail.PNRNo) return BadRequest("ID mismatch");

            var existingDetail = await _passengerDetailService.GetPassengerDetailByIdAsync(id, userId);

            if (existingDetail == null) return NotFound();

            passengerDetail.UserID = userId;

            try
            {
                var updatedPassengerDetail = await _passengerDetailService.UpdatePassengerDetailAsync(passengerDetail);
                return Ok(updatedPassengerDetail);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePassengerDetail(long id)
        {
            if (!TryGetUserId(out int userId))
            {
                return Unauthorized("Invalid User ID.");
            }

            var result = await _passengerDetailService.DeletePassengerDetailAsync(id, userId);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
