using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hectre
{
    public abstract class BaseLogic : IDisposable
    {
        // Set context 
        protected HectreContext DB = new HectreContext();

        public void Dispose()
        {
            DB.Dispose();
        }
    }
}
