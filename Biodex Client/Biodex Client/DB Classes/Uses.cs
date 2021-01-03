using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class Uses
    {
        public long Biodexreport_id { get; set; }
        public long Exercise_id { get; set; }
        public long Settings_id { get; set; }

        public Uses(long biodexreport_id, long exercise_id, long settings_id)
        {
            Biodexreport_id = biodexreport_id;
            Exercise_id = exercise_id;
            Settings_id = settings_id;
        }
    }
}
