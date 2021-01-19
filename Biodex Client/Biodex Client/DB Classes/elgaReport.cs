using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class ElgaReport
    {
        public long nElgaID { get; set; }

        public ElgaReport(long nElgaID)
        {
            this.nElgaID = nElgaID;
        }
    }
}
