using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class givesInformation
    {
        public long ElgaID { get; set; }
        public long PersonalDataID { get; set; }
        public long MedDataId { get; set; }

        public givesInformation(long elgaID, long personalDataID, long medDataId)
        {
            ElgaID = elgaID;
            PersonalDataID = personalDataID;
            MedDataId = medDataId;
        }
    }
}
