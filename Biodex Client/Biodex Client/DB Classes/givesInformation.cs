using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class givesInformation
    {
        public long _elgaID { get; set; }
        public long _personalDataID { get; set; }
        public long _medDataId { get; set; }

        public givesInformation(long elgaID, long personalDataID, long medDataId)
        {
            _elgaID = elgaID;
            _personalDataID = personalDataID;
            _medDataId = medDataId;
        }
    }
}
