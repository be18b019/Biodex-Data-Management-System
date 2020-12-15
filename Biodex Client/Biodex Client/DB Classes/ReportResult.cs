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

        public DateTime Date { get; set; }

        public ReportResult(long reportID, DateTime date)
        {
            ReportID = reportID;
            Date = date;
        }
    }
}
