using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class receives
    {
        public long _matriculationNumber { get; set; }
        public long _reportID { get; set; }

        public receives(long matriculationNumber, long reportID)
        {
            _matriculationNumber = matriculationNumber;
            _reportID = reportID;
        }
    }
}
