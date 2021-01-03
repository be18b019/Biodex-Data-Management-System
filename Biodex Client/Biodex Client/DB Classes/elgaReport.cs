using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class ElgaReport
    {
        public long ElgaID { get; set; }

        public ElgaReport(long elgaID)
        {
            this.ElgaID = elgaID;
        }
    }
}
