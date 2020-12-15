using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class Proband
    {
        public long _MatriculationNumber { get; set; }

        public string _Name { get; set; }

        public Proband(long matriculationNumber, string name)
        {
            _MatriculationNumber = matriculationNumber;
            _Name = name;
        }
    }
}
