using Microsoft.AspNetCore.Mvc;
using OnlineRailroad.Models;
using OnlineRailroad.Services;

namespace OnlineRailroad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FareRuleController : ControllerBase
    {
        private readonly IFareRuleService _fareRuleService;

        public FareRuleController(IFareRuleService fareRuleService)
        {
            _fareRuleService = fareRuleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFareRules()
        {
            var fareRules = await _fareRuleService.GetAllFareRulesAsync();
            return Ok(fareRules);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFareRuleById(int id)
        {
            var fareRule = await _fareRuleService.GetFareRuleByIdAsync(id);
            if (fareRule == null) return NotFound(new { Message = "Fare rule not found." });
            return Ok(fareRule);
        }

        [HttpPost]
        public async Task<IActionResult> AddFareRule([FromBody] FareRule fareRule)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _fareRuleService.AddFareRuleAsync(fareRule);
                return CreatedAtAction(nameof(GetFareRuleById), new { id = fareRule.FareRuleID }, fareRule);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while adding the fare rule." });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFareRule(int id, [FromBody] FareRule fareRule)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != fareRule.FareRuleID) return BadRequest(new { Message = "Fare rule ID mismatch." });

            try
            {
                await _fareRuleService.UpdateFareRuleAsync(fareRule);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the fare rule." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFareRule(int id)
        {
            try
            {
                await _fareRuleService.DeleteFareRuleAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the fare rule." });
            }
        }
    }
}
