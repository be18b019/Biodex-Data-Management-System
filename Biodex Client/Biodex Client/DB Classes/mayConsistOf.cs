using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class mayConsistOf
    {
        public long _reportID { get; set; }
        public long _elgaID { get; set; }
        public long _biodexReportID { get; set; }

        public mayConsistOf(long reportID, long elgaID, long biodexReportID)
        {
            _reportID = reportID;
            _elgaID = elgaID;
            _biodexReportID = biodexReportID;
        }
    }
}
