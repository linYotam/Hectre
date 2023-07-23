using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hectre
{
    public class OrchardsLogic : BaseLogic
    {
        // Get all orchards from DB
        public List<OrchardModel> GetAllOrchads()
        {
            return DB.Orchards.Select(o => new OrchardModel(o)).ToList();
        }
    }
}
