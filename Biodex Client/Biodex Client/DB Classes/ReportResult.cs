using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class ReportResult
    {
        public long nReportID { get; set; }
        public string sMatriculationNumber { get; set; }

        public DateTime aDate { get; set; }

        public ReportResult(long nReportID, DateTime aDate, string sMatriculationNumber)
        {
            this.nReportID = nReportID;
            this.aDate = aDate;
            this.sMatriculationNumber = sMatriculationNumber;
        }
    }
}
