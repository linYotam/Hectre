using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hectre
{
    public class TimesheetsLogic : BaseLogic
    {
        // Add new timesheet to DB
        public TimesheetModel AddNewTimesheet(TimesheetModel timesheetModel)
        {

            // Check if Orchard already exists. if not --> create a new one.
            Orchard? existingOrchard = DB.Orchards.SingleOrDefault(o => o.Id == timesheetModel.Orchard.Id);

            Orchard orchard;

            if (existingOrchard != null)
            {
                // Orchard already exists, use the existing one.
                orchard = existingOrchard;
            }
            else
            {
                // Orchard does not exist, create a new one based on the provided parameters.
                orchard = new Orchard
                {
                    Id = timesheetModel.Orchard.Id,
                    Name = timesheetModel.Orchard.Name,
                    Block = timesheetModel.Orchard.Block,
                    SubBlock = timesheetModel.Orchard.SubBlock
                };

                DB.Orchards.Add(orchard);
            }

            Timesheet timesheet = timesheetModel.ConvertToTimesheet();
            timesheet.Orchard = orchard;

            DB.Timesheets.Add(timesheet);
            DB.SaveChanges();

            timesheetModel.Id = timesheet.Id;
            return timesheetModel;
        }
    }
}
