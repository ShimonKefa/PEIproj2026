using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIPEITESTE01.Services;
using APIPEITESTE01.Entities;


namespace APIPEITESTE01.API.Controllers
{
    [Route("API/[Controller]")]
    [ApiController]
    public class AnamneseController : ControllerBase
    {
        private readonly InsertServices _insert = new InsertServices();
        private readonly UpdateServices _update = new UpdateServices();
        private readonly DeleteServices _delete = new DeleteServices();

        //Insert Anamnese
        [HttpPost]
        public IActionResult InsertAnamnese([FromBody] Anamnese anamnese)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var insert = _insert.InsertAnamnese(anamnese);
            return Ok(insert);
        }

       //Update anamnese
       [HttpPut("{ID}/UpdateAnamnese")]
       public IActionResult UpdateAnamnese(Guid ID,[FromBody] Anamnese anamnese)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updt = _update.UpdateAnamnese(ID, anamnese);
            return Ok(updt);
        }
        
        //Delete Anamnese
        [HttpDelete("{ID}/DeleteAnamnese")]
        public IActionResult DeleteAnamnese(Guid ID)
        {  
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           var del =_delete.DeleteAnamnese(ID);
           return Ok(del); 
        }

    }
}