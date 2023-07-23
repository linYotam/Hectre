using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hectre
{
    [HarvestPayloadFormat] 
    public class HarvestModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Orchard is required.")]
        public OrchardModel Orchard { get; set; }

        [Required(ErrorMessage = "SupervisorId is required.")]
        public Guid SupervisorId { get; set; }

        [Required(ErrorMessage = "OrchardId is required.")]
        public Guid OrchardId { get; set; }

        [Required(ErrorMessage = "PickerId is required.")]
        public Guid PickerId { get; set; }

        [Required(ErrorMessage = "PickingDate is required.")]
        public DateTime PickingDate { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        public string Type { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "BinCount must be greater than 0.")]
        public int BinCount { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "HourlyWageRate must be greater than 0.")]
        public decimal HourlyWageRate { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "HoursWorked must be greater than or equal to 0.")]
        public int HoursWorked { get; set; }

        [Required(ErrorMessage = "Variety is required.")]
        public string Variety { get; set; }


        public HarvestModel(){}

        // Set all HarvestModel params
        public HarvestModel(Harvest harvest)
        {
            Id= harvest.Id;
            SupervisorId = harvest.SupervisorId;
            PickerId = harvest.PickerId;
            PickingDate = harvest.PickingDate;
            Type = harvest.Type;
            BinCount = harvest.BinCount;
            HourlyWageRate = harvest.HourlyWageRate;
            HoursWorked = harvest.HoursWorked;
            Variety = harvest.Variety;
            OrchardId = harvest.OrchardId;

        }

        // Convert HarvestModel params to Harvest params
        public Harvest ConvertToHarvest()
        {
            Harvest harvest = new Harvest
            {
                Id= Id,
                OrchardId = Orchard.Id,
                SupervisorId = SupervisorId,
                PickerId = PickerId,
                PickingDate = PickingDate,
                Type = Type,
                BinCount = BinCount,
                HourlyWageRate = HourlyWageRate,
                HoursWorked = HoursWorked,
                Variety = Variety
            };
            return harvest;
        }
    }
}
