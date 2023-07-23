using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Hectre
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrchardsController : ControllerBase, IDisposable
    {
        private readonly OrchardsLogic logic = new OrchardsLogic();

        [HttpGet]
        [Authorize(Roles = "customer,admin")]
        public IActionResult GetAllOrchards()
        {
            try 
            {
                // Get all orchards
                List<OrchardModel> orchards = logic.GetAllOrchads();
                return Ok(orchards);
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error response.
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public void Dispose()
        {
            logic.Dispose();
        }
    }
}
