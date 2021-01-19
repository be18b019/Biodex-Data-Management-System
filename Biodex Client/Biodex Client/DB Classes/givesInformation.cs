using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class GivesInformation
    {
        public long nElgaID { get; set; }
        public long nPersonalDataID { get; set; }
        public long nMedDataId { get; set; }

        public GivesInformation(long nElgaID, long nPersonalDataID, long nMedDataId)
        {
            this.nElgaID = nElgaID;
            this.nPersonalDataID = nPersonalDataID;
            this.nMedDataId = nMedDataId;
        }
    }
}
