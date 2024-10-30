using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineRailroad.Models;
using OnlineRailroad.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineRailroad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainController : ControllerBase
    {
        private readonly ITrainService _trainService;

        public TrainController(ITrainService trainService)
        {
            _trainService = trainService;
        }

        // Allow anonymous users to get all trains
        [HttpGet]
        public async Task<IActionResult> GetAllTrains()
        {
            var trains = await _trainService.GetAllTrainsAsync();
            return Ok(trains);
        }

        // Allow anonymous users to get train by number
        [HttpGet("{trainNo}")]
        public async Task<IActionResult> GetTrainByNo(string trainNo)
        {
            var train = await _trainService.GetTrainByNoAsync(trainNo);
            if (train == null) return NotFound();
            return Ok(train);
        }

        // Allow only ROLE_ADMIN and ROLE_EMPLOYEE to create trains
        [HttpPost]
        [Authorize(Roles = "ROLE_ADMIN,ROLE_EMPLOYEE")]
        public async Task<IActionResult> AddTrain(Train train)
        {
            await _trainService.AddTrainAsync(train);
            return Ok();
        }

        // Allow only ROLE_ADMIN and ROLE_EMPLOYEE to update trains
        [HttpPut("{trainNo}")]
        [Authorize(Roles = "ROLE_ADMIN,ROLE_EMPLOYEE")]
        public async Task<IActionResult> UpdateTrain(string trainNo, Train train)
        {
            await _trainService.UpdateTrainAsync(trainNo, train);
            return Ok();
        }

        // Allow only ROLE_ADMIN and ROLE_EMPLOYEE to delete trains
        [HttpDelete("{trainNo}")]
        [Authorize(Roles = "ROLE_ADMIN,ROLE_EMPLOYEE")]
        public async Task<IActionResult> DeleteTrain(string trainNo)
        {
            await _trainService.DeleteTrainAsync(trainNo);
            return Ok();
        }

        // Allow anonymous users to find trains by name
        [HttpGet("by-name/{trainName}")]
        public async Task<IActionResult> GetTrainsByName(string trainName)
        {
            var trains = await _trainService.GetTrainsByNameAsync(trainName);
            if (trains == null || !trains.Any()) return NotFound(new { message = "No trains found with that name." });
            return Ok(trains);
        }
    }
}
