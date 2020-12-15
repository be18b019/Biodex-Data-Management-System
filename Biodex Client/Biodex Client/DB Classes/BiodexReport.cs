using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class BiodexReport
    {
        public long BiodexReportID { get; set; }
        public double Torque { get; set; }
        public double AngularSpeed { get; set; }
        public double Force { get; set; }
        public string Exercise { get; set; }
        public string Muscle { get; set; }
        public string Repetition { get; set; }

        public BiodexReport(long biodexReportID, double torque, double angularSpeed, double force, string exercise, string muscle, string repetition)
        {
            BiodexReportID = biodexReportID;
            Torque = torque;
            AngularSpeed = angularSpeed;
            Force = force;
            Exercise = exercise;
            Muscle = muscle;
            Repetition = repetition;
        }
    }
}
