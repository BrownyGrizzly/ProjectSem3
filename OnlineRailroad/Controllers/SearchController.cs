using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineRailroad.Models;
using OnlineRailroad.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineRailroad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("trains")]
        public IActionResult SearchTrains(int sourceStationId, int destinationStationId, DateTime travelDate)
        {
            try
            {
                var trains = _searchService.SearchTrains(sourceStationId, destinationStationId, travelDate);
                return Ok(trains);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("distance")]
        public IActionResult CalculateDistance(int sourceStationId, int destinationStationId, string trainId, DateTime travelDate)
        {
            try
            {
                var distance = _searchService.CalculateTotalDistance(sourceStationId, destinationStationId, trainId, travelDate);
                return Ok(new { DistanceKm = distance });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("available-seats")]
        public async Task<ActionResult<List<int>>> GetAvailableSeats(string trainNo, DateTime travelDate)
        {
            try
            {
                // Call the search service to get available seats
                var availableSeats = await _searchService.GetAvailableSeatsAsync(trainNo, travelDate);
                return Ok(availableSeats);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
