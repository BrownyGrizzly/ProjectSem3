using Microsoft.AspNetCore.Mvc;
using OnlineRailroad.Services;
using Route = OnlineRailroad.Models.Route;

namespace OnlineRailroad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _routeService;

        public RouteController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoutes()
        {
            var routes = await _routeService.GetAllRoutesAsync();
            return Ok(routes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRouteById(int id)
        {
            var route = await _routeService.GetRouteByIdAsync(id);
            if (route == null) return NotFound(new { Message = "Route not found." });
            return Ok(route);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoute([FromBody] Route route)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _routeService.AddRouteAsync(route);
                return CreatedAtAction(nameof(GetRouteById), new { id = route.RouteID }, route);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while adding the route." });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoute(int id, [FromBody] Route route)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != route.RouteID) return BadRequest(new { Message = "Route ID mismatch." });

            try
            {
                await _routeService.UpdateRouteAsync(route);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the route." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoute(int id)
        {
            try
            {
                await _routeService.DeleteRouteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the route." });
            }
        }
    }
}
