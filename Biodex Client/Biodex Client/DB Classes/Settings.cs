using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class Settings
    {
        public long nSettingsID { get; set; }
        public string sPowerheadOrientation { get; set; }
        public string sPowerheadHeight { get; set; }
        public string sPowerheadPosition { get; set; }
        public string sPowerheadAttachment { get; set; }
        public string sPowerheadTilt { get; set; }
        public string sSeatHeight { get; set; }
        public string sSeatOrientation { get; set; }
        public string sSeatTilt { get; set; }
        public string sSeatPosition { get; set; }
        public string sControllerMode { get; set; }
        public string sControllerCushion { get; set; }
        public string sControllerSensitivity { get; set; }
        public string sControllerROMUpperLimit { get; set; }
        public string sControllerROMLowerLimit { get; set; }
        public string sControllerROMPercentage { get; set; }
        public string sControllerEccentricSpeed { get; set; }
        public string sControllerPassiveSpeed { get; set; }
        public string sControllerTorqueLimits { get; set; }
        public string sControllerPause { get; set; }
        public string sControllerIsokineticSpeed { get; set; }
        public string sHipFlexion { get; set; }
        public string sFootplateTilt { get; set; }
        public string sKneeFlexion { get; set; }
        public string sAnkleFlexion { get; set; }
        public string sShoulderAbduction { get; set; }
        public string sShoulderFlexion { get; set; }
        public string sElbowFlexion { get; set; }

        public Settings(long nSettingsID, string sPowerheadOrientation, string sPowerheadHeight, string sPowerheadPosition, string sPowerheadAttachment, string sPowerheadTilt, string sSeatHeight, string sSeatOrientation, string sSeatTilt, string sSeatPosition, string sHipFlexion, string sFootplateTilt, string sKneeFlexion, string sAnkleFlexion, string sShoulderAbduction, string sShoulderFlexion, string sControllerMode, string sControllerCushion, string sControllerSensitivity, string sControllerROMUpperLimit, string sControllerROMLowerLimit, string sControllerROMPercentage, string sControllerEccentricSpeed, string sControllerPassiveSpeed, string sControllerTorqueLimits, string sControllerPause, string sControllerIsokineticSpeed,  string sElbowFlexion)
        {
            this.nSettingsID = nSettingsID;
            this.sPowerheadOrientation = sPowerheadOrientation;
            this.sPowerheadHeight = sPowerheadHeight;
            this.sPowerheadPosition = sPowerheadPosition;
            this.sPowerheadAttachment = sPowerheadAttachment;
            this.sPowerheadTilt = sPowerheadTilt;
            this.sSeatHeight = sSeatHeight;
            this.sSeatOrientation = sSeatOrientation;
            this.sSeatTilt = sSeatTilt;
            this.sSeatPosition = sSeatPosition;
            this.sControllerMode = sControllerMode;
            this.sControllerCushion = sControllerCushion;
            this.sControllerSensitivity = sControllerSensitivity;
            this.sControllerROMUpperLimit = sControllerROMUpperLimit;
            this.sControllerROMLowerLimit = sControllerROMLowerLimit;
            this.sControllerROMPercentage = sControllerROMPercentage;
            this.sControllerEccentricSpeed = sControllerEccentricSpeed;
            this.sControllerPassiveSpeed = sControllerPassiveSpeed;
            this.sControllerTorqueLimits = sControllerTorqueLimits;
            this.sControllerPause = sControllerPause;
            this.sControllerIsokineticSpeed = sControllerIsokineticSpeed;
            this.sHipFlexion = sHipFlexion;
            this.sFootplateTilt = sFootplateTilt;
            this.sKneeFlexion = sKneeFlexion;
            this.sAnkleFlexion = sAnkleFlexion;
            this.sShoulderAbduction = sShoulderAbduction;
            this.sShoulderFlexion = sShoulderFlexion;
            this.sElbowFlexion = sElbowFlexion;
        }
    }
}
