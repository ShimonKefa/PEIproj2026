using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIPEITESTE01.Services;
using APIPEITESTE01.Entities;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace APIPEITESTE01.API.Controllers
{
    [Route("API/[Controller]")]
    [ApiController]

    public class ClientController : ControllerBase
    {
        private readonly InsertServices _insert = new InsertServices();
        private readonly UpdateServices _update = new UpdateServices();
        private readonly SelectServices _select = new SelectServices();
        private readonly DeleteServices _delete = new DeleteServices();

        //insert Client
        [HttpPost]
        public IActionResult InsertClient([FromBody] Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);    
            }

            var insert = _insert.InsertClient(client);
            return Ok(insert);
        }
        //readAll
        [HttpGet]
        public IActionResult ReadAll()
        {
            var read = _select.ReadAllClients();
            return Ok(read);
        }
        //readClient
        [HttpGet("{ID}/readClient")]
        public IActionResult ReadClient(Guid ID)
        {
            var read1 = _select.GetClientbyID(ID);
            return Ok(read1);
        }
        //Update Client
        [HttpPut("{ID}/UpdateClient")]
        public IActionResult UpdateClient(Guid ID, [FromBody] Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updt = _update.UpdateClient(ID, client);
            return Ok(updt);
        }
        //Delete Client
        [HttpDelete("{ID}/DeleteClient")]
        public IActionResult DeleteClient(Guid ID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           var del = _delete.DeleteClient(ID);
            return Ok(del);
        }
        
    }
}