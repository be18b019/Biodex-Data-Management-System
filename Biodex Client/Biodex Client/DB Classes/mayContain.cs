using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class MayContain
    {
        public long nReportID { get; set; }
        public long nElgaID { get; set; }
        public long nBiodexReportID { get; set; }

        public MayContain(long nReportID, long nElgaID, long nBiodexReportID)
        {
            this.nReportID = nReportID;
            this.nElgaID = nElgaID;
            this.nBiodexReportID = nBiodexReportID;
        }
    }
}
