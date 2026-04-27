using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIPEITESTE01.Services;
using APIPEITESTE01.Entities;

namespace APIPEITESTE01.API.Controllers
{
    [Route("API/[Controller]")]
    [ApiController]

    public class ComorbityController : ControllerBase
    {
        private readonly InsertServices _insert = new InsertServices();
        private readonly UpdateServices _update = new UpdateServices();
        private readonly DeleteServices _delete = new DeleteServices();

        //insert comorbity
        [HttpPost]
        public IActionResult InsertComorbity([FromBody] Comorbity comorbity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var insert = _insert.InsertComorbity(comorbity);
            return Ok(insert);
        }

        //update Comorbity
        [HttpPut("{ID}/UpdateComorbity")]
        public IActionResult UpdateComorbity(Guid ID,[FromBody] Comorbity comorbity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           var updt = _update.UpdateComorbitiy(ID, comorbity);
            return Ok(updt);
        }
        
        //Delete Comorbity
        [HttpDelete("{ID}/DeleteComorbity")]
        public IActionResult DeleteComorbity(Guid ID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var del = _delete.DeleteComorbities(ID);
            return Ok(del);
        }
    }
}