using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Hectre
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TimesheetPayloadFormatAttribute : ValidationAttribute
    {
        // Set JSON structre
        private readonly string timesheetSchemaJson = @"
            {
                ""$schema"": ""http://json-schema.org/draft-07/schema#"",
                ""type"": ""object"",
                ""properties"": {
                    ""Orchard"": {
                        ""type"": ""object"",
                        ""properties"": {
                            ""Id"": { ""type"": ""string"", ""format"": ""uuid"" },
                            ""Name"": { ""type"": ""string"" },
                            ""Block"": { ""type"": ""integer"" },
                            ""SubBlock"": { ""type"": ""string"" }
                        },
                        ""required"": [""Id"", ""Name"", ""Block"", ""SubBlock""]
                    },
                    ""SupervisorId"": { ""type"": ""string"", ""format"": ""uuid"" },
                    ""PickerId"": { ""type"": ""string"", ""format"": ""uuid"" },
                    ""StartTime"": { ""type"": ""string"", ""format"": ""date-time"" },
                    ""EndTime"": { ""type"": ""string"", ""format"": ""date-time"" },
                    ""Activity"": { ""type"": ""string"" }
                },
                ""required"": [""Orchard"", ""SupervisorId"", ""PickerId"", ""StartTime"", ""EndTime"", ""Activity""]
            }

        ";

        // Check JSON foramt validation 
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                // The payload is null, which means it's not in the correct format.
                return false;
            }

            try
            {

                string json = JsonConvert.SerializeObject(value);

                // Load the predefined JSON schema for Harvest.
                JSchema schema = JSchema.Parse(timesheetSchemaJson);

                // Parse the JSON payload to a JObject.
                JObject timesheetJson = JObject.Parse(json);

                // Validate the JSON payload against the schema.
                bool isValid = timesheetJson.IsValid(schema);

                // Check for missing required fields in the JSON payload.
                IList<string> requiredProperties = schema.Required;
                bool hasMissingRequiredFields = requiredProperties.Any(prop => !timesheetJson.ContainsKey(prop));
                if (hasMissingRequiredFields)
                {  
                    return false; 
                }

                return isValid;
            }
            catch (Exception ex)
            {
                ErrorMessage = "Invalid payload format: " + ex.Message;
                return false;
            }
        }
    }
}
