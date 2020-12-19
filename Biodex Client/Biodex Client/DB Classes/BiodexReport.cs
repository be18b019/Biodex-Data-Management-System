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
        public float[] Torque { get; set; }
        public float[] Angle { get; set; }
        public float[] AngleVelocity { get; set; }
        public string Exercise { get; set; }
        public string Muscle { get; set; }
        public string Repetition { get; set; }
        public long SettingsID { get; set; }

        public BiodexReport(long biodexReportID, float[] torque, float[] angle, float[] angleVelocity, string exercise, string muscle, string repetition, long settingsID)
        {
            BiodexReportID = biodexReportID;
            Torque = torque;
            //TO-DO Rishad: Tabellennamen ändern!!! ANFANG
            Angle = angle;
            AngleVelocity = angleVelocity;
            //TO-DO Rishad: Tabellennamen ändern!!! ENDE
            Exercise = exercise;
            Muscle = muscle;
            Repetition = repetition;
            SettingsID = settingsID;
        }
    }
}
