using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using APIPEITESTE01.Services;
using APIPEITESTE01.Entities;

namespace APIPEITESTE01.API.Controllers
{
    [Route("API/[Controller]")]
    [ApiController]

    public class WorkOutController : ControllerBase
    {
        private readonly InsertServices _insert = new InsertServices();
        private readonly UpdateServices _update = new UpdateServices();
        private readonly SelectServices _select = new SelectServices();
        private readonly DeleteServices _delete = new DeleteServices();

        //Read
        [HttpGet("workouts/{clientID}")]
        public IActionResult GetWorkOuts(Guid clientID)
        {
            var workOuts = _select.GetWorkOuts(clientID);
            if (workOuts == null || !workOuts.Any()) return NotFound();
            return Ok(workOuts);
        }

        //Insert
        [HttpPost("generate-workout/{clientID}/{daysPerWeek}")]
        public async Task<IActionResult> GenerateWorkOut(Guid clientID, int daysPerWeek, [FromBody] WorkOut workOut)
        {
            var aiService = new AIService();
            var result = await aiService.AIRequest(clientID, workOut, daysPerWeek);
            return Ok(result);
        }

        // Update
        [HttpPut("workouts/{workOutID}")]
        public IActionResult UpdateWorkOut(Guid workOutID, [FromBody] WorkOut workOut)
        {
            var result = _update.UpdateWorkOut(workOutID, workOut);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // Delete
        [HttpDelete("workouts/{workOutID}")]
        public IActionResult DeleteWorkOut(Guid workOutID)
        {
            var result = _delete.DeleteWorkout(workOutID);
            if (result == null) return NotFound();
            return Ok(result);
        }

    }
}