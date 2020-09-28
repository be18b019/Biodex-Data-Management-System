use biodex_version1;
DROP TABLE IF EXISTS relevantValues;
DROP TABLE IF EXISTS Seat;
DROP TABLE IF EXISTS Powerhead;
DROP TABLE IF EXISTS Settings;
DROP TABLE IF EXISTS OptionalExercise;
DROP TABLE IF EXISTS KneeExtensionFlexion;
DROP TABLE IF EXISTS Exercise;
DROP TABLE IF EXISTS BiodexSystem;
DROP TABLE IF EXISTS BiodexReport;
DROP TABLE IF EXISTS personalData;
DROP TABLE IF EXISTS hospitalinformation;
DROP TABLE IF EXISTS diagnosis;
DROP TABLE IF EXISTS medicalData;
DROP TABLE IF EXISTS elgaMedicalReport;
DROP TABLE IF EXISTS report;
DROP TABLE IF EXISTS proband;


CREATE TABLE proband (matriculationNumber BIGINT PRIMARY KEY, Vorname TEXT NOT NULL);

CREATE TABLE report (ID INT PRIMARY KEY AUTO_INCREMENT NOT NULL, probandMatriculationNumber BIGINT, FOREIGN KEY (probandMatriculationNumber) REFERENCES proband(matriculationNumber));

CREATE TABLE elgaMedicalReport (ID INT PRIMARY KEY, reportProbandMatriculationNumber BIGINT, 
FOREIGN KEY (ID) REFERENCES report(ID),
FOREIGN KEY (reportProbandMatriculationNumber) REFERENCES report(probandMatriculationNumber));
CREATE TABLE medicalData (ID INT PRIMARY KEY, admissionReason TEXT, anamnesis TEXT, previousDisease TEXT, riskANDalergies TEXT, medicationAtArrival TEXT, medicationDurringStay TEXT, actionsByHospital TEXT,
FOREIGN KEY (ID) REFERENCES elgaMedicalReport(ID));
CREATE TABLE diagnosis (ID INT PRIMARY KEY, stateAtRelease TEXT, physicalIssue TEXT, recommendedMEasurement TEXT, rehaAIM TEXT, funtureMedication TEXT, summary TEXT,
FOREIGN KEY (ID) REFERENCES elgaMedicalReport(ID));
CREATE TABLE hospitalinformation (ID INT PRIMARY KEY, department TEXT, startDate DATE, endDate DATE, hospitalAdress DATE, admissionNumber INT, hospitalName TEXT, hospitalContact INT, resposibleDoctor TEXT,
FOREIGN KEY (ID) REFERENCES elgaMedicalReport(ID));
CREATE TABLE personalData (ID INT PRIMARY KEY, familyStatus TEXT, Email TEXT, adress TEXT, legalGuardian TEXT, religion TEXT, laguage TEXT, insurance TEXT, telNumber INT, birthPlace TEXT, birthDATE DATE, gender TEXT, SvNummer BIGINT, NameTitel TEXT,
FOREIGN KEY (ID) REFERENCES elgaMedicalReport(ID),
FOREIGN KEY (SvNummer) REFERENCES elgaMedicalReport(reportProbandMatriculationNumber));

CREATE TABLE BiodexReport (ReportID INT PRIMARY KEY,
FOREIGN KEY (ReportID) REFERENCES Report(ID));
CREATE TABLE BiodexSystem (BiodexName VARCHAR (100), ReportID INT, PRIMARY KEY (BiodexName, ReportID), 
FOREIGN KEY (ReportID) REFERENCES BiodexReport(ReportID));

CREATE TABLE Exercise (ID INT PRIMARY KEY, Torque FLOAT, AngularSpeed FLOAT, Power FLOAT, Muscle TEXT, ROMLimit FLOAT, Mode INT);
CREATE TABLE KneeExtensionFlexion (ExerciseID INT PRIMARY KEY, Sensitivity FLOAT, 
FOREIGN KEY (ExerciseID) REFERENCES Exercise(ID));
CREATE TABLE OptionalExercise(ExerciseID INT PRIMARY KEY, OptionalExercise TEXT, 
FOREIGN KEY (ExerciseID) REFERENCES Exercise(ID));
CREATE TABLE Settings (ID INT PRIMARY KEY);
CREATE TABLE Powerhead(SettingsID INT PRIMARY KEY, ShaftPosition FLOAT, HorizontalPosition FLOAT, Tilt FLOAT, Height FLOAT, 
FOREIGN KEY(SettingsID) REFERENCES Settings(ID));
CREATE TABLE Seat(SettingsID INT PRIMARY KEY, Position FLOAT, Rotation FLOAT, Tilt FLOAT, Height FLOAT, 
FOREIGN KEY(SettingsID) REFERENCES Settings(ID));

CREATE TABLE relevantValues(BiodexSystemName VARCHAR (100), SettingsID INT, ExerciseID INT, PRIMARY KEY(BiodexSystemName, SettingsID, ExerciseID), 
FOREIGN KEY (BiodexSystemName) REFERENCES BiodexSystem(BiodexName), FOREIGN KEY (SettingsID) REFERENCES Settings(ID), FOREIGN KEY (ExerciseID) REFERENCES Exercise(ID));
