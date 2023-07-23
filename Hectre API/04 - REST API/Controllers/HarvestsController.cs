using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hectre
{

    [Route("api/[controller]")]
    [ApiController]
    public class HarvestsController : ControllerBase, IDisposable
    {
        private readonly HarvestsLogic logic = new HarvestsLogic();

        /*public HarvestsController()
        {
            
        }

        public HarvestsController(HarvestsLogic logic)
        {
            this.logic = logic;
        }*/

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult CreateHarvest(HarvestModel harvestModel)
        { 

            try
            {
                // Check JSON format
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // If validation passes, store the Harvest record in the database.
                HarvestModel addedHarvest = logic.AddNewHarvest(harvestModel);  

                // Return a 201 Created response with the created Harvest record in the response body.
                return Created("api/harvests/" + addedHarvest.Id, addedHarvest);
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error response.
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "customer,admin")]
        public IActionResult GetAllHarvests()
        {
            try
            {
                // Get all harvests
                List<HarvestModel> harvests = logic.GetAllHarvests();
                return Ok(harvests);
            }
            catch(Exception ex)
            {
                // Return a 500 Internal Server Error response.
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetHarvestsByOrchardAndDateRange")]
        [Authorize(Roles = "customer,admin")]
        public IActionResult GetHarvestsByOrchardAndDate(string orchardIds, DateTime startDate, DateTime endDate)
        {

            string[] orchardsGuids = orchardIds.Split(',');

            try
            {
                // Get all harvests by orchards ids and date range
                List<HarvestModel> harvests = logic.GetHarvestsByOrchardAndDate(orchardsGuids, startDate, endDate);

                if (harvests == null)
                {
                    // If no Harvests were found, return a 404 Not Found response.
                    return NotFound($"No Harvests found for Orchard Ids: {orchardsGuids} within the specified date range: {startDate} - {endDate}.");
                }

                // If Harvests were found, return a 200 OK response with the Harvests in the response body.
                return Ok(harvests);
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
