using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hectre
{

    //[TimesheetPayloadFormat]
    public class TimesheetModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Orchard is required.")]
        public OrchardModel Orchard { get; set; }

        [Required(ErrorMessage = "SupervisorId is required.")]
        public Guid SupervisorId { get; set; }

        [Required(ErrorMessage = "PickerId is required.")]
        public Guid PickerId { get; set; }

        [Required(ErrorMessage = "StartTime is required.")]
        public string StartTime { get; set; }

        [Required(ErrorMessage = "EndTime is required.")]
        public string EndTime { get; set; }

        [Required(ErrorMessage = "Activity is required.")]
        public string Activity { get; set; } = null!;

        public TimesheetModel() { }

        public TimesheetModel(Timesheet timesheet)
        {
            Id = timesheet.Id;
            SupervisorId = timesheet.SupervisorId;
            PickerId = timesheet.PickerId;
            StartTime = timesheet.StartTime;
            EndTime = timesheet.EndTime;
            Activity = timesheet.Activity;
        }

        public Timesheet ConvertToTimesheet()
        {
            Timesheet timesheet = new Timesheet
            {
                Id = Id,
                OrchardId = Orchard.Id,
                SupervisorId = SupervisorId,
                PickerId = PickerId,
                StartTime = StartTime,
                EndTime = EndTime,
                Activity = Activity,
            };
            return timesheet;
        }
    }
}
