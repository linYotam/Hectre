using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hectre
{
    public class HarvestsLogic : BaseLogic
    {

        public HarvestsLogic() { }

        public HarvestsLogic(HectreContext dbContext)
        {
            DB = dbContext;
        }

        // Get all harvests from DB
        public List<HarvestModel> GetAllHarvests()
        {
            return DB.Harvests.Select(h => new HarvestModel(h)).ToList();
        }

        // Get all harvests by orchards ids and date range
        public List<HarvestModel> GetHarvestsByOrchardAndDate(string[] orchardIds, DateTime startDate, DateTime endDate)
        {

            if (orchardIds.Length == 1 && orchardIds[0] == "0")
            {
                // If orchardIds array contains only one value of "0", return all orchards.
                if (startDate == DateTime.MinValue && endDate == DateTime.MinValue)
                {
                    return DB.Harvests.Select(harvest => new HarvestModel(harvest)).ToList();
                }
                else if (startDate == DateTime.MinValue)
                {
                    return DB.Harvests.Where(harvest => harvest.PickingDate <= endDate).Select(harvest => new HarvestModel(harvest)).ToList();
                }
                else if (endDate == DateTime.MinValue)
                {
                    return DB.Harvests.Where(harvest => harvest.PickingDate >= startDate).Select(harvest => new HarvestModel(harvest)).ToList();
                }
                else
                {
                    return DB.Harvests.Where(harvest => harvest.PickingDate >= startDate && harvest.PickingDate <= endDate).Select(harvest => new HarvestModel(harvest)).ToList();
                }
            }
            else
            {
                // Convert the string array of orchardIds to Guid list
                List<Guid> orchardGuids = orchardIds.Select(id => Guid.Parse(id)).ToList();

                // Filter the harvests based on orchardIds and date range
                if (startDate == DateTime.MinValue && endDate == DateTime.MinValue)
                {
                    return DB.Harvests.Where(harvest => orchardGuids.Contains(harvest.OrchardId)).Select(harvest => new HarvestModel(harvest)).ToList();
                }
                else if (startDate == DateTime.MinValue)
                {
                    return DB.Harvests.Where(harvest => orchardGuids.Contains(harvest.OrchardId) && harvest.PickingDate <= endDate).Select(harvest => new HarvestModel(harvest)).ToList();
                }
                else if (endDate == DateTime.MinValue)
                {
                    return DB.Harvests.Where(harvest => orchardGuids.Contains(harvest.OrchardId) && harvest.PickingDate >= startDate).Select(harvest => new HarvestModel(harvest)).ToList();
                }
                else
                {
                    return DB.Harvests.Where(harvest => orchardGuids.Contains(harvest.OrchardId) && harvest.PickingDate >= startDate && harvest.PickingDate <= endDate).Select(harvest => new HarvestModel(harvest)).ToList();
                }
            }
        }

        // Add new harvest
        public HarvestModel AddNewHarvest(HarvestModel harvestModel)
        {

            // Check if Orchard already exists. if not --> create a new one.
            Orchard? existingOrchard = DB.Orchards.SingleOrDefault(o => o.Id == harvestModel.Orchard.Id);

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
                    Id = harvestModel.Orchard.Id,
                    Name = harvestModel.Orchard.Name,
                    Block = harvestModel.Orchard.Block,
                    SubBlock = harvestModel.Orchard.SubBlock
                };

                DB.Orchards.Add(orchard);
            }

            Harvest harvest = harvestModel.ConvertToHarvest();
            harvest.Orchard = orchard;

            DB.Harvests.Add(harvest);
            DB.SaveChanges();

            harvestModel.Id = harvest.Id;
            return harvestModel;
        }
    }
}
