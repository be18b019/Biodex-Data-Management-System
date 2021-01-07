using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class Proband
    {
        public string MatriculationNumber { get; set; }

        public string ProbandName { get; set; }

        public Proband(string matriculationNumber, string name)
        {
            MatriculationNumber = matriculationNumber;
            ProbandName = name;
        }
    }
}
