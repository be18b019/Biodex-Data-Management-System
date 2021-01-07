using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class BiodexReport
    {
        public long BiodexReportID { get; set; }
        
        public long SettingsID { get; set; }

        public BiodexReport(long biodexReportID, long settingsID)
        {
            BiodexReportID = biodexReportID;
            SettingsID = settingsID;
        }
    }
}
