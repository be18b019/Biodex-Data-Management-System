using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class Settings
    {
        public long SettingsID { get; set; }
        public string Powerhead_ORIENTATION { get; set; }
        public string Powerhead_HEIGHT { get; set; }
        public string Powerhead_POSITION { get; set; }
        public string Powerhead_ATTACHMENT { get; set; }
        public string Powerhead_TILT { get; set; }
        public string Seat_HEIGHT { get; set; }
        public string Seat_ORIENTATION { get; set; }
        public string Seat_TILT { get; set; }
        public string Seat_POSITION { get; set; }
        public string Controller_MODE { get; set; }
        public string Controller_CUSHION { get; set; }
        public string Controller_SENSITIVITY { get; set; }
        public string Controller_ROM_UPPER_LIMIT { get; set; }
        public string Controller_ROM_LOWER_LIMIT { get; set; }
        public string Controller_ROM_PERCENTAGE { get; set; }
        public string Controller_ECCENTRIC_SPEED { get; set; }
        public string Controller_PASSIVE_SPEED { get; set; }
        public string Controller_TOURQUE_LIMITS { get; set; }
        public string Controller_PAUSE { get; set; }
        public string Controller_ISOKINETIC_SPEED { get; set; }
        public string Hip_FLEXION { get; set; }
        public string Footplate_TILT { get; set; }
        public string Knee_FLEXION { get; set; }
        public string Ankle_FLEXION { get; set; }
        public string Shoulder_ABDUCTION { get; set; }
        public string Shoulder_FLEXION { get; set; }
        public string ELBOW_FLEXION { get; set; }

        public Settings(long settingsID, string powerhead_ORIENTATION, string powerhead_HEIGHT, string powerhead_POSITION, string powerhead_ATTACHMENT, string powerhead_TILT, string seat_HEIGHT, string seat_ORIENTATION, string seat_TILT, string seat_POSITION, string hip_FLEXION, string footplate_TILT, string knee_FLEXION, string ankle_FLEXION, string shoulder_ABDUCTION, string shoulder_FLEXION, string controller_MODE, string controller_CUSHION, string controller_SENSITIVITY, string controller_ROM_UPPER_LIMIT, string controller_ROM_LOWER_LIMIT, string controller_ROM_PERCENTAGE, string controller_ECCENTRIC_SPEED, string controller_PASSIVE_SPEED, string controller_TOURQUE_LIMITS, string controller_PAUSE, string controller_ISOKINETIC_SPEED,  string eLBOW_FLEXION)
        {
            SettingsID = settingsID;
            Powerhead_ORIENTATION = powerhead_ORIENTATION;
            Powerhead_HEIGHT = powerhead_HEIGHT;
            Powerhead_POSITION = powerhead_POSITION;
            Powerhead_ATTACHMENT = powerhead_ATTACHMENT;
            Powerhead_TILT = powerhead_TILT;
            Seat_HEIGHT = seat_HEIGHT;
            Seat_ORIENTATION = seat_ORIENTATION;
            Seat_TILT = seat_TILT;
            Seat_POSITION = seat_POSITION;
            Controller_MODE = controller_MODE;
            Controller_CUSHION = controller_CUSHION;
            Controller_SENSITIVITY = controller_SENSITIVITY;
            //TO-DO Rishad upper and lower limit!!! ANFANG
            Controller_ROM_UPPER_LIMIT = controller_ROM_UPPER_LIMIT;
            Controller_ROM_LOWER_LIMIT = controller_ROM_LOWER_LIMIT;
            //TO-DO ENDE
            Controller_ROM_PERCENTAGE = controller_ROM_PERCENTAGE;
            Controller_ECCENTRIC_SPEED = controller_ECCENTRIC_SPEED;
            Controller_PASSIVE_SPEED = controller_PASSIVE_SPEED;
            Controller_TOURQUE_LIMITS = controller_TOURQUE_LIMITS;
            Controller_PAUSE = controller_PAUSE;
            Controller_ISOKINETIC_SPEED = controller_ISOKINETIC_SPEED;
            Hip_FLEXION = hip_FLEXION;
            Footplate_TILT = footplate_TILT;
            Knee_FLEXION = knee_FLEXION;
            Ankle_FLEXION = ankle_FLEXION;
            Shoulder_ABDUCTION = shoulder_ABDUCTION;
            Shoulder_FLEXION = shoulder_FLEXION;
            //TO-DO Rishad elbow flexion hinzufügen!!!
            ELBOW_FLEXION = eLBOW_FLEXION;
        }
    }
}
