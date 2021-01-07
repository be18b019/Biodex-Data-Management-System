using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class MedicalData
    {
        public long MedicalDataID { get; set; }
        public string HospitalStartDate { get; set; }
        public string HospitalEndDate { get; set; }
        public string HospitalAdress { get; set; }
        public string HospitalDepartment { get; set; }
        public string HospitalAdmissionNumber { get; set; }
        public string HospitalName { get; set; }
        public string HospitalContact { get; set; }
        public string HospitalResposibleDoctor { get; set; }
        public string MedAdmissionReason { get; set; }
        public string MedAnamnesis { get; set; }
        public string MedPreviousDisease { get; set; }
        public string MedRiskAlergies { get; set; }
        public string MedMedicationAtArrival { get; set; }
        public string MedMedicationDurringStay { get; set; }
        public string MedActionsByHospital { get; set; }
        public string DiagPhysicalIssue { get; set; }
        public string DiagRecommendedMeasurement { get; set; }
        public string DiagRehaAim { get; set; }
        public string DiagFutureMedication { get; set; }
        public string DiagSummary { get; set; }
        public string DiagStateAtRelease { get; set; }

        public MedicalData(long medicalDataID, string hospitalStartDate, string hospitalEndDate, string hospitalAdress, string hospitalDepartment, string hospitalAdmissionNumber, string hospitalName, string hospitalContact, string hospitalResposibleDoctor, string diagStateAtRelease, string diagSummary, string diagFutureMedication, string diagRehaAim, string diagRecommendedMeasurement, string diagPhysicalIssue,  string medActionsByHospital, string medMedicationDurringStay, string medMedicationAtArrival, string medRiskAlergies, string medPreviousDisease, string medAnamnesis, string medAdmissionReason)
        {
            this.MedicalDataID = medicalDataID;
            this.HospitalStartDate = hospitalStartDate;
            this.HospitalEndDate = hospitalEndDate;
            this.HospitalAdress = hospitalAdress;
            this.HospitalDepartment = hospitalDepartment;
            this.HospitalAdmissionNumber = hospitalAdmissionNumber;
            this.HospitalName = hospitalName;
            this.HospitalContact = hospitalContact;
            this.HospitalResposibleDoctor = hospitalResposibleDoctor;
            this.MedAdmissionReason = medAdmissionReason;
            this.MedAnamnesis = medAnamnesis;
            this.MedPreviousDisease = medPreviousDisease;
            this.MedRiskAlergies = medRiskAlergies;
            this.MedMedicationAtArrival = medMedicationAtArrival;
            this.MedMedicationDurringStay = medMedicationDurringStay;
            this.MedActionsByHospital = medActionsByHospital;
            this.DiagPhysicalIssue = diagPhysicalIssue;
            this.DiagRecommendedMeasurement = diagRecommendedMeasurement;
            this.DiagRehaAim = diagRehaAim;
            this.DiagFutureMedication = diagFutureMedication;
            this.DiagSummary = diagSummary;
            this.DiagStateAtRelease = diagStateAtRelease;
        }
    }
}
