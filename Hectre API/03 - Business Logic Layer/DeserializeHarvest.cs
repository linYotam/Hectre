using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hectre
{
    public class DeserializeHarvest
    {
        // Deserialize Harvest from JSON
        public void DeserializeHarvestModelFromJson()
        {
            string json = @"{
            ""Orchard"": {
                ""Id"": ""769e6571-100e-4a18-a577-34a49f0c8ed3"",
                ""Name"": ""Super Foods"",
                ""Block"": 17,
                ""SubBlock"": ""22B""
            },
            ""SupervisorId"": ""87892a43-d96b-4b90-8c4c-925d07fe07fb"",
            ""PickerId"": ""d7895f81-66b1-11ed-a52c-0242c0a85002"",
            ""PickingDate"": ""2023-06-01T12:30:00Z"",
            ""Type"": ""Bin"",
            ""BinCount"": 3,
            ""HourlyWageRate"": 22.50,
            ""HoursWorked"": 5,
            ""Variety"": ""Royal Gala""
        }";

            // Deserialize the JSON string into a HarvestModel object.
            HarvestModel harvestModel = JsonConvert.DeserializeObject<HarvestModel>(json);
        }
    }
}
