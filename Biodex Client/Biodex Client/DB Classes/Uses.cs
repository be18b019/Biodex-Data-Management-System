using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class Uses
    {
        public long nBiodexReportID { get; set; }
        public long nExerciseID { get; set; }
        public long nSettingsID { get; set; }

        public Uses(long nBiodexReportID, long nExerciseID, long nSettingsID)
        {
            this.nBiodexReportID = nBiodexReportID;
            this.nExerciseID = nExerciseID;
            this.nSettingsID = nSettingsID;
        }
    }
}
