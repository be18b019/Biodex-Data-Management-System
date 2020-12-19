!
!!
!!!
!!!!
!!!!!!
!!!!!!!! ACHTUNG: DROP TABLE LÖSCHT ALLE EINTRÄGE, DIE SIND DANN KOMPLETT WEG, FOR EVER
!!!!!!
!!!!
!!!
!!
!

DROP DATABASE IF EXISTS BiodexV2;
CREATE DATABASE BiodexV2;
\c biodexv2;

DROP TABLE IF EXISTS medicalDATA;
DROP TABLE IF EXISTS personalDATA;
DROP TABLE IF EXISTS elgaReport;
DROP TABLE IF EXISTS biodex_report;
DROP TABLE IF EXISTS settings;
DROP TABLE IF EXISTS report_result;
DROP TABLE IF EXISTS proband;





CREATE TABLE proband( 
    matriculation_number VARCHAR(50) NOT NULL PRIMARY KEY,
    proband_name VARCHAR(50));

INSERT INTO proband(matriculation_number, proband_name) 
VALUES ('be18b500', 'COOLER NAME');
SELECT * FROM proband;


CREATE TABLE report_result(
    report_ID BIGSERIAL NOT NULL PRIMARY KEY,
    matriculation_number VARCHAR(50) REFERENCES proband(matriculation_number),
    date_time TIMESTAMP);
    
INSERT INTO report_result(matriculation_number, date_time) 
VALUES ('be18b500', '2016-06-22 19:10:25-07');
SELECT * FROM report_result;


CREATE TABLE settings(
    settings_ID BIGSERIAL NOT NULL PRIMARY KEY,
    Powerhead_ORIENTATION VARCHAR(50),
    Powerhead_HEIGHT VARCHAR(50),
    Powerhead_POSITION VARCHAR(50),
    Powerhead_ATTACHMENT VARCHAR(50),
    Powerhead_TILT VARCHAR(50),
    Seat_HEIGHT VARCHAR(50),
    Seat_ORIENTATION VARCHAR(50),
    Seat_TILT VARCHAR(50),
    Seat_POSITION VARCHAR(50),
    Hip_Flexion VARCHAR(50),
    Footplate_TILT VARCHAR(50),
    Knee_FLEXION VARCHAR(50),
    Ankle_FLEXION VARCHAR(50),
    Shoulder_ABDUCTION VARCHAR(50),
    Shoulder_FLEXION VARCHAR(50),
    Controller_MODE VARCHAR(50),
    Controller_CUSHION VARCHAR(50),
    Controller_SENSITIVITY VARCHAR(50),
    Controller_ROM_LIMIT VARCHAR(50),
    Controller_ROM_PERCENTAGE VARCHAR(50),
    Controller_ECCENTRIC_SPEED VARCHAR(50),
    Controller_PASSIVE_SPEED VARCHAR(50),
    Controller_TORQUE_LIMITS VARCHAR(50),
    Controller_PAUSE VARCHAR(50),
    Controller_ISOKINETIC_SPEED VARCHAR(50));

INSERT INTO settings(Powerhead_ORIENTATION, Controller_SENSITIVITY) 
VALUES ('90Degree', 'Class A');
SELECT * FROM settings;


CREATE TABLE biodex_report(
    biodexreport_ID BIGSERIAL NOT NULL PRIMARY KEY, 
    angular_speed REAL [],
    force REAL [],
    torque REAL [],
    exercise VARCHAR(50),
    repetition VARCHAR(50),
    muscle VARCHAR(50),
    settings_ID BIGINT REFERENCES settings(settings_ID));

INSERT INTO biodex_report(exercise, muscle, settings_ID) 
VALUES ('BicepCurls', 'Bicep, Tricep, Latissimus', 1);
SELECT * FROM biodex_report;


CREATE TABLE elgaReport(
    elga_ID BIGSERIAL NOT NULL PRIMARY KEY);

INSERT INTO elgaReport(elga_ID) VALUES ('100');
SELECT * FROM elgaReport;


CREATE TABLE personalDATA(
    SV_Number BIGSERIAL NOT NULL PRIMARY KEY,
    familyStatus VARCHAR(50),
    Email VARCHAR(50),
    adress VARCHAR(50),
    legalGuardian VARCHAR(50),
    religion VARCHAR(50),
    language VARCHAR(50),
    insurance VARCHAR(50),
    telNumber VARCHAR(50),
    birthPlace VARCHAR(50),
    birthDATE VARCHAR(50),
    gender VARCHAR(50),
    NameTitel VARCHAR(50));

INSERT INTO personalDATA(familyStatus, Email, language, telNumber, insurance)
VALUES ('Mutter', 'technikum@wien.at', 'Deutsch', '0133', 'WGKK');
SELECT * FROM personalDATA;


CREATE TABLE medicalDATA(
    medicalDATA_ID BIGSERIAL NOT NULL PRIMARY KEY,
    hospitalStartDate VARCHAR(50),
    hospitalEndDate VARCHAR(50),
    hospitalAdress VARCHAR(50),
    hospitalDepartment VARCHAR(50),
    hospitalAdmissionNumber VARCHAR(50),
    hospitalName VARCHAR(50),
    hospitalContact VARCHAR(50),
    hospitalResponsibleDoctor VARCHAR(50),
    diagStateAtRelease VARCHAR(50),
    diagSummary VARCHAR(50),
    diagFutureMedication VARCHAR(50),
    diagRehaAim VARCHAR(50),
    diagRecommendedMeasurement VARCHAR(50),
    diagPhysicalIssue VARCHAR(50),
    medActionsByHospital VARCHAR(50),
    medMedicationDuringStay VARCHAR(50),
    medMedicationAtArrival VARCHAR(50),
    medRiskAlergies VARCHAR(50),
    medPreviousDisease VARCHAR(50),
    medAnamnesis VARCHAR(50),
    medAdmissionReason VARCHAR(50));

INSERT INTO medicalDATA(hospitalAdress, diagPhysicalIssue, medRiskAlergies)
VALUES ('Michelbeuern', 'Beinbruch', 'Penicillin');
SELECT * FROM medicalDATA;


CREATE TABLE mayCONTAIN(
    report_ID BIGINT REFERENCES report_result(report_ID),
    biodexreport_ID BIGINT REFERENCES biodex_report(biodexreport_ID),
    elga_ID BIGINT REFERENCES elgaReport(elga_ID),
    PRIMARY KEY (report_ID, biodexreport_ID, elga_ID));

INSERT INTO mayCONTAIN VALUES (1,1,100);
SELECT * FROM mayCONTAIN;


CREATE TABLE givesInforamtion(
    elga_ID BIGINT REFERENCES elgaReport(elga_ID),
    SV_Number BIGINT REFERENCES personalDATA(SV_Number),
    medicalDATA_ID BIGINT REFERENCES medicalDATA(medicalDATA_ID),
    PRIMARY KEY (elga_ID, SV_Number, medicalDATA_ID));

INSERT INTO givesInforamtion VALUES (100,1,1);
SELECT * FROM givesInforamtion;


