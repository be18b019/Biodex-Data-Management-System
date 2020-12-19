using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class mayContain
    {
        public long ReportID { get; set; }
        public long ElgaID { get; set; }
        public long BiodexReportID { get; set; }

        public mayContain(long reportID, long elgaID, long biodexReportID)
        {
            ReportID = reportID;
            ElgaID = elgaID;
            BiodexReportID = biodexReportID;
        }
    }
}
