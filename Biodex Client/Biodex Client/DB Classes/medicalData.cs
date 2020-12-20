using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class medicalData
    {
        public long medicalDataID { get; set; }
        public string hospitalStartDate { get; set; }
        public string hospitalEndDate { get; set; }
        public string hospitalAdress { get; set; }
        public string hospitalDepartment { get; set; }
        public string hospitalAdmissionNumber { get; set; }
        public string hospitalName { get; set; }
        public string hospitalContact { get; set; }
        public string hospitalResposibleDoctor { get; set; }
        public string medAdmissionReason { get; set; }
        public string medAnamnesis { get; set; }
        public string medPreviousDisease { get; set; }
        public string medRiskAlergies { get; set; }
        public string medMedicationAtArrival { get; set; }
        public string medMedicationDurringStay { get; set; }
        public string medActionsByHospital { get; set; }
        public string diagPhysicalIssue { get; set; }
        public string diagRecommendedMeasurement { get; set; }
        public string diagRehaAim { get; set; }
        public string diagFutureMedication { get; set; }
        public string diagSummary { get; set; }
        public string diagStateAtRelease { get; set; }

        public medicalData(long medicalDataID, string hospitalStartDate, string hospitalEndDate, string hospitalAdress, string hospitalDepartment, string hospitalAdmissionNumber, string hospitalName, string hospitalContact, string hospitalResposibleDoctor, string diagStateAtRelease, string diagSummary, string diagFutureMedication, string diagRehaAim, string diagRecommendedMeasurement, string diagPhysicalIssue,  string medActionsByHospital, string medMedicationDurringStay, string medMedicationAtArrival, string medRiskAlergies, string medPreviousDisease, string medAnamnesis, string medAdmissionReason)
        {
            this.medicalDataID = medicalDataID;
            this.hospitalStartDate = hospitalStartDate;
            this.hospitalEndDate = hospitalEndDate;
            this.hospitalAdress = hospitalAdress;
            this.hospitalDepartment = hospitalDepartment;
            this.hospitalAdmissionNumber = hospitalAdmissionNumber;
            this.hospitalName = hospitalName;
            this.hospitalContact = hospitalContact;
            this.hospitalResposibleDoctor = hospitalResposibleDoctor;
            this.medAdmissionReason = medAdmissionReason;
            this.medAnamnesis = medAnamnesis;
            this.medPreviousDisease = medPreviousDisease;
            this.medRiskAlergies = medRiskAlergies;
            this.medMedicationAtArrival = medMedicationAtArrival;
            this.medMedicationDurringStay = medMedicationDurringStay;
            this.medActionsByHospital = medActionsByHospital;
            this.diagPhysicalIssue = diagPhysicalIssue;
            this.diagRecommendedMeasurement = diagRecommendedMeasurement;
            this.diagRehaAim = diagRehaAim;
            this.diagFutureMedication = diagFutureMedication;
            this.diagSummary = diagSummary;
            this.diagStateAtRelease = diagStateAtRelease;
        }
    }
}
