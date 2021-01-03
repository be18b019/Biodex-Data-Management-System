using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class GivesInformation
    {
        public long ElgaID { get; set; }
        public long PersonalDataID { get; set; }
        public long MedDataId { get; set; }

        public GivesInformation(long elgaID, long personalDataID, long medDataId)
        {
            ElgaID = elgaID;
            PersonalDataID = personalDataID;
            MedDataId = medDataId;
        }
    }
}
