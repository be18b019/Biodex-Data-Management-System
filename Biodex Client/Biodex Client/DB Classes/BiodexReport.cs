using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class BiodexReport
    {
        public long nBiodexReportID { get; set; }
        
        public long nSettingsID { get; set; }

        public BiodexReport(long nBiodexReportID, long nSettingsID)
        {
            this.nBiodexReportID = nBiodexReportID;
            this.nSettingsID = nSettingsID;
        }
    }
}
