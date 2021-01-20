using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class MedicalData
    {
        public long nMedicalDataID { get; set; }
        public string sHospitalStartDate { get; set; }
        public string sHospitalEndDate { get; set; }
        public string sHospitalAdress { get; set; }
        public string sHospitalDepartment { get; set; }
        public string sHospitalAdmissionNumber { get; set; }
        public string sHospitalName { get; set; }
        public string sHospitalContact { get; set; }
        public string sHospitalResposibleDoctor { get; set; }
        public string sMedAdmissionReason { get; set; }
        public string sMedAnamnesis { get; set; }
        public string sMedPreviousDisease { get; set; }
        public string sMedRiskAlergies { get; set; }
        public string sMedMedicationAtArrival { get; set; }
        public string sMedMedicationDurringStay { get; set; }
        public string sMedActionsByHospital { get; set; }
        public string sDiagPhysicalIssue { get; set; }
        public string sDiagRecommendedMeasurement { get; set; }
        public string sDiagRehaAim { get; set; }
        public string sDiagFutureMedication { get; set; }
        public string sDiagSummary { get; set; }
        public string sDiagStateAtRelease { get; set; }

        public MedicalData(long nMedicalDataID, string sHospitalStartDate, string sHospitalEndDate, string sHospitalAdress, string sHospitalDepartment, string sHospitalAdmissionNumber, string sHospitalName, string sHospitalContact, string sHospitalResposibleDoctor, string sDiagStateAtRelease, string sDiagSummary, string sDiagFutureMedication, string sDiagRehaAim, string sDiagRecommendedMeasurement, string sDiagPhysicalIssue,  string sMedActionsByHospital, string sMedMedicationDurringStay, string sMedMedicationAtArrival, string sMedRiskAlergies, string sMedPreviousDisease, string sMedAnamnesis, string sMedAdmissionReason)
        {
            this.nMedicalDataID = nMedicalDataID;
            this.sHospitalStartDate = sHospitalStartDate;
            this.sHospitalEndDate = sHospitalEndDate;
            this.sHospitalAdress = sHospitalAdress;
            this.sHospitalDepartment = sHospitalDepartment;
            this.sHospitalAdmissionNumber = sHospitalAdmissionNumber;
            this.sHospitalName = sHospitalName;
            this.sHospitalContact = sHospitalContact;
            this.sHospitalResposibleDoctor = sHospitalResposibleDoctor;
            this.sMedAdmissionReason = sMedAdmissionReason;
            this.sMedAnamnesis = sMedAnamnesis;
            this.sMedPreviousDisease = sMedPreviousDisease;
            this.sMedRiskAlergies = sMedRiskAlergies;
            this.sMedMedicationAtArrival = sMedMedicationAtArrival;
            this.sMedMedicationDurringStay = sMedMedicationDurringStay;
            this.sMedActionsByHospital = sMedActionsByHospital;
            this.sDiagPhysicalIssue = sDiagPhysicalIssue;
            this.sDiagRecommendedMeasurement = sDiagRecommendedMeasurement;
            this.sDiagRehaAim = sDiagRehaAim;
            this.sDiagFutureMedication = sDiagFutureMedication;
            this.sDiagSummary = sDiagSummary;
            this.sDiagStateAtRelease = sDiagStateAtRelease;
        }
    }
}
