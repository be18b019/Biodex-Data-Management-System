using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class useSettings
    {
        public long _biodexreportID { get; set; }
        public long _settingsID { get; set; }

        public useSettings(long biodexreportID, long settingsID)
        {
            _biodexreportID = biodexreportID;
            _settingsID = settingsID;
        }
    }
}
