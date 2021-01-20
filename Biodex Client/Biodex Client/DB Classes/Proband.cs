using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class Proband
    {
        public string sMatriculationNumber { get; set; }

        public string sProbandName { get; set; }

        public Proband(string sMatriculationNumber, string sProbandName)
        {
            this.sMatriculationNumber = sMatriculationNumber;
            this.sProbandName = sProbandName;
        }
    }
}
