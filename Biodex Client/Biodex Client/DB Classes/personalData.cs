using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class PersonalData
    {
        public string SV_Number { get; set; }
        public string FamilyStatus { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string LegalGuardian { get; set; }
        public string Religion { get; set; }
        public string Laguage { get; set; }
        public string Insurance { get; set; }
        public string TelNumber { get; set; }
        public string BirthPlace { get; set; }
        public string BirthDATE { get; set; }
        public string Gender { get; set; }
        public string NameTitel { get; set; }

        public PersonalData(string sV_Number, string familyStatus, string email, string adress, string legalGuardian, string religion, string language, string insurance, string telNumber, string birthPlace, string birthDATE, string gender, string nameTitel)
        {
            SV_Number = sV_Number;
            this.FamilyStatus = familyStatus;
            Email = email;
            this.Adress = adress;
            this.LegalGuardian = legalGuardian;
            this.Religion = religion;
            this.Laguage = Laguage;
            this.Insurance = insurance;
            this.TelNumber = telNumber;
            this.BirthPlace = birthPlace;
            this.BirthDATE = birthDATE;
            this.Gender = gender;
            NameTitel = nameTitel;
        }
    }
}
