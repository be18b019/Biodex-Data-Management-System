using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class ReportResult
    {
        public long ReportID { get; set; }
        public string MatriculationNumber { get; set; }

        public DateTime Date { get; set; }

        public ReportResult(long reportID, DateTime date, string matriculationNumber)
        {
            ReportID = reportID;
            Date = date;
            MatriculationNumber = matriculationNumber;
        }
    }
}
