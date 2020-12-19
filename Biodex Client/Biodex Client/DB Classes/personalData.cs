using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class personalData
    {
        public string SV_Number { get; set; }
        public string familyStatus { get; set; }
        public string Email { get; set; }
        public string adress { get; set; }
        public string legalGuardian { get; set; }
        public string religion { get; set; }
        public string laguage { get; set; }
        public string insurance { get; set; }
        public string telNumber { get; set; }
        public string birthPlace { get; set; }
        public string birthDATE { get; set; }
        public string gender { get; set; }
        public string NameTitel { get; set; }

        public personalData(string sV_Number, string familyStatus, string email, string adress, string legalGuardian, string religion, string language, string insurance, string telNumber, string birthPlace, string birthDATE, string gender, string nameTitel)
        {
            //Rishad ändern, SV Nummer kann nicht BIGSERIAL SEIN!! zB 7229200890 BITTE VARCHAR(10)!!!
            SV_Number = sV_Number;
            this.familyStatus = familyStatus;
            Email = email;
            this.adress = adress;
            this.legalGuardian = legalGuardian;
            this.religion = religion;
            this.laguage = laguage;
            this.insurance = insurance;
            this.telNumber = telNumber;
            this.birthPlace = birthPlace;
            this.birthDATE = birthDATE;
            this.gender = gender;
            NameTitel = nameTitel;
        }
    }
}
