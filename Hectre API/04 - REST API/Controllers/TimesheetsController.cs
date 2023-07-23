using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hectre
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetsController : ControllerBase, IDisposable
    {
        private readonly TimesheetsLogic logic = new TimesheetsLogic();

        [HttpPost]
        public IActionResult CreateTimesheet(TimesheetModel timesheetModel)
        {

            try
            {
                // Check JSON format
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // If validation passes, store the timesheet record in the database.
                TimesheetModel addedTimesheet = logic.AddNewTimesheet(timesheetModel);

                // Return a 201 Created response with the created Timesheet record in the response body.
                return Created("api/timesheets/" + addedTimesheet.Id, addedTimesheet);
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
