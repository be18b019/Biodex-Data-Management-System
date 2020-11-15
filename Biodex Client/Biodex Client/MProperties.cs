using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client
{
    /*
     * class to store all meta data besides actual data from Biodex
     */
    class MProperties
    {
        //Exercise Properties
        public string EExercise { get; set; }
        public string EMuscle { get; set; }
        public string ERepetitions { get; set; }

        //Powerhead Properties
        public string POrientation { get; set; }
        public string PTilt { get; set; }
        public string PHeight { get; set; }
        public string PPosition { get; set; }
        public string PAttachments { get; set; } //multiple?

        //Chair Properties
        public string COrientation { get; set; }
        public string CTilt { get; set; }
        public string CHeight { get; set; }
        public string CPosition { get; set; }

        //Controller Properties
        public string CoMode { get; set; }
        public string CoCushion { get; set; }
        public string CoSensitivity { get; set; }
        public string CoPause { get; set; }
        public string CoEccentricSpeed { get; set; }
        public string CoPassiveSpeed { get; set; }
        public string CoIsokineticSpeed { get; set; }
        public string CoTorqueLimit { get; set; }
        public string CoPercentROM { get; set; }
        public string CoROMLower { get; set; }
        public string CoROMUpper { get; set; }

        //Setup Properties
        public string SHipFlexion { get; set; }
        public string SFootPlateTilt { get; set; }
        public string SAnkleFlexion { get; set; }
        public string SKneeFlexion { get; set; }
        public string SShoulderAbduction { get; set; }
        public string SShoulderFlexion { get; set; }
        public string SElbowFlexion { get; set; }

        public MProperties(string eExercise, string eMuscle, string eRepetitions, string pOrientation,
                           string pTilt, string pHeight, string pPosition, string pAttachments,
                           string cOrientation, string cTilt, string cHeight, string cPosition,
                           string coMode, string coCushion, string coSensitivity, string coPause,
                           string coEccentricSpeed, string coPassiveSpeed, string coIsokineticSpeed, string coTorqueLimit,
                           string coPercentROM, string coROMLower, string coROMUpper, string sHipFlexion,
                           string sFootPlateTilt, string sAnkleFlexion, string sKneeFlexion, string sShoulderAbduction,
                           string sShoulderFlexion, string sElbowFlexion)
        {
            EExercise = eExercise;
            EMuscle = eMuscle;
            ERepetitions = eRepetitions;
            POrientation = pOrientation;
            PTilt = pTilt;
            PHeight = pHeight;
            PPosition = pPosition;
            PAttachments = pAttachments;
            COrientation = cOrientation;
            CTilt = cTilt;
            CHeight = cHeight;
            CPosition = cPosition;
            CoMode = coMode;
            CoCushion = coCushion;
            CoSensitivity = coSensitivity;
            CoPause = coPause;
            CoEccentricSpeed = coEccentricSpeed;
            CoPassiveSpeed = coPassiveSpeed;
            CoIsokineticSpeed = coIsokineticSpeed;
            CoTorqueLimit = coTorqueLimit;
            CoPercentROM = coPercentROM;
            CoROMLower = coROMLower;
            CoROMUpper = coROMUpper;
            SHipFlexion = sHipFlexion;
            SFootPlateTilt = sFootPlateTilt;
            SAnkleFlexion = sAnkleFlexion;
            SKneeFlexion = sKneeFlexion;
            SShoulderAbduction = sShoulderAbduction;
            SShoulderFlexion = sShoulderFlexion;
            SElbowFlexion = sElbowFlexion;
        }
    }
}
