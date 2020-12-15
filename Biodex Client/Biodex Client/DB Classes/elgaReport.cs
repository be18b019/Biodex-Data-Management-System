using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class elgaReport
    {
        public long elgaID { get; set; }

        public elgaReport(long elgaID)
        {
            this.elgaID = elgaID;
        }
    }
}
