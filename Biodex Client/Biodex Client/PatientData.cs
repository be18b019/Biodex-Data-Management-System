using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client
{
    class PatientData
    {
        //Personal Data
        public string PDNameTitle { get; set; }
        public string PDSVNumber { get; set; }
        public string PDGender { get; set; }
        public string PDBDateBirth { get; set; }
        public string PDPlaceBirth { get; set; }
        public string PDPhoneNumber { get; set; }
        public string PDInsurance { get; set; }
        public string PDSLanguage { get; set; }
        public string PDReligion { get; set; }
        public string PDLegalGuardian { get; set; }
        public string PDSAddress { get; set; }
        public string PDEmail { get; set; }
        public string PDFamilyStatus { get; set; }

        //Hospital Information
        public string HIHospitalName { get; set; }
        public string HIDepartment{ get; set; }
        public string HIHospitalAddress { get; set; }
        public string HIHospitalContact { get; set; }
        public string HIStartDate { get; set; }
        public string HIEndDate{ get; set; }
        public string HIAdmissionNumber { get; set; }
        public string HIResponsibleDoctor { get; set; }

        //Medical Data
        public string MDAdmissionReason{ get; set; }
        public string MDAnamnesis { get; set; }
        public string MDPreviousDiseases { get; set; }
        public string MDRisksAllergies { get; set; }
        public string MDMedicationArrival { get; set; }
        public string MDMedicationStay { get; set; }
        public string MDActionsHospital{ get; set; }

        //Diagnosis
        public string DStateRelease { get; set; }
        public string DPhysicalIssue { get; set; }
        public string DRecommendedMeasurements { get; set; }
        public string DRehabilitationAim { get; set; }
        public string DFutureMedication { get; set; }
        public string DSummary { get; set; }

        public PatientData(string pDNameTitle, string pDSVNumber, string pDGender, string pDBDateBirth, 
                           string pDPlaceBirth, string pDPhoneNumber, string pDInsurance, string pDSLanguage, 
                           string pDReligion, string pDLegalGuardian, string pDSAddress, string pDEmail, 
                           string pDFamilyStatus, string hIHospitalName, string hIDepartment, string hIHospitalAddress, 
                           string hIHospitalContact, string hIStartDate, string hIEndDate, string hIAdmissionNumber, 
                           string hIResponsibleDoctor, string mDAdmissionReason, string mDAnamnesis, string mDPreviousDiseases, 
                           string mDRisksAllergies, string mDMedicationArrival, string mDMedicationStay, string mDActionsHospital, 
                           string dStateRelease, string dPhysicalIssue, string dRecommendedMeasurements, string dRehabilitationAim, 
                           string dFutureMedication, string dSummary)
        {
            PDNameTitle = pDNameTitle;
            PDSVNumber = pDSVNumber;
            PDGender = pDGender;
            PDBDateBirth = pDBDateBirth;
            PDPlaceBirth = pDPlaceBirth;
            PDPhoneNumber = pDPhoneNumber;
            PDInsurance = pDInsurance;
            PDSLanguage = pDSLanguage;
            PDReligion = pDReligion;
            PDLegalGuardian = pDLegalGuardian;
            PDSAddress = pDSAddress;
            PDEmail = pDEmail;
            PDFamilyStatus = pDFamilyStatus;
            HIHospitalName = hIHospitalName;
            HIDepartment = hIDepartment;
            HIHospitalAddress = hIHospitalAddress;
            HIHospitalContact = hIHospitalContact;
            HIStartDate = hIStartDate;
            HIEndDate = hIEndDate;
            HIAdmissionNumber = hIAdmissionNumber;
            HIResponsibleDoctor = hIResponsibleDoctor;
            MDAdmissionReason = mDAdmissionReason;
            MDAnamnesis = mDAnamnesis;
            MDPreviousDiseases = mDPreviousDiseases;
            MDRisksAllergies = mDRisksAllergies;
            MDMedicationArrival = mDMedicationArrival;
            MDMedicationStay = mDMedicationStay;
            MDActionsHospital = mDActionsHospital;
            DStateRelease = dStateRelease;
            DPhysicalIssue = dPhysicalIssue;
            DRecommendedMeasurements = dRecommendedMeasurements;
            DRehabilitationAim = dRehabilitationAim;
            DFutureMedication = dFutureMedication;
            DSummary = dSummary;
        }

    }
}
