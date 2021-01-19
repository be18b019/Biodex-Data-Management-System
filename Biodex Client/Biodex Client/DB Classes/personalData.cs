using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class PersonalData
    {
        public string sSVNumber { get; set; }
        public string sFamilyStatus { get; set; }
        public string sEmail { get; set; }
        public string sAddress { get; set; }
        public string sLegalGuardian { get; set; }
        public string sReligion { get; set; }
        public string sLanguage { get; set; }
        public string sInsurance { get; set; }
        public string sTelNumber { get; set; }
        public string sBirthPlace { get; set; }
        public string sBirthDate { get; set; }
        public string sGender { get; set; }
        public string sNameTitle { get; set; }

        public PersonalData(string sSVNumber, string sFamilyStatus, string sEmail, string sAddress, string sLegalGuardian, string sReligion, string sLanguage, string sInsurance, string sTelNumber, string sBirthPlace, string sBirthDate, string sGender, string sNameTitle)
        {
            this.sSVNumber = sSVNumber;
            this.sFamilyStatus = sFamilyStatus;
            this.sEmail = sEmail;
            this.sAddress = sAddress;
            this.sLegalGuardian = sLegalGuardian;
            this.sReligion = sReligion;
            this.sLanguage = sLanguage;
            this.sInsurance = sInsurance;
            this.sTelNumber = sTelNumber;
            this.sBirthPlace = sBirthPlace;
            this.sBirthDate = sBirthDate;
            this.sGender = sGender;
            this.sNameTitle = sNameTitle;
        }
    }
}
