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

DROP TABLE IF EXISTS uses;
DROP TABLE IF EXISTS givesInforamtion;
DROP TABLE IF EXISTS mayCONTAIN;
DROP TABLE IF EXISTS medicalDATA;
DROP TABLE IF EXISTS personalDATA;
DROP TABLE IF EXISTS elgaReport;
DROP TABLE IF EXISTS exercise_data;
DROP TABLE IF EXISTS settings;
DROP TABLE IF EXISTS biodex_report;
DROP TABLE IF EXISTS report_result;






CREATE TABLE report_result(
    report_ID BIGSERIAL NOT NULL PRIMARY KEY,
    date_time TIMESTAMP);
    
SELECT * FROM report_result;



CREATE TABLE biodex_report(
    biodexreport_ID BIGSERIAL NOT NULL PRIMARY KEY);

SELECT * FROM biodex_report;



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
    Elbow_FLEXION VARCHAR(50),
    Controller_MODE VARCHAR(50),
    Controller_CUSHION VARCHAR(50),
    Controller_SENSITIVITY VARCHAR(50),
    Controller_ROM_UPPER_LIMIT VARCHAR(50),
    Controller_ROM_LOWER_LIMIT VARCHAR(50),
    Controller_ROM_PERCENTAGE VARCHAR(50),
    Controller_ECCENTRIC_SPEED VARCHAR(50),
    Controller_PASSIVE_SPEED VARCHAR(50),
    Controller_TORQUE_LIMITS VARCHAR(50),
    Controller_PAUSE VARCHAR(50),
    Controller_ISOKINETIC_SPEED VARCHAR(50));

SELECT * FROM settings;



CREATE TABLE exercise_data(
    exercise_ID BIGSERIAL NOT NULL PRIMARY KEY,
    torque REAL [],
    angle REAL [],
    velocity REAL [],
    muscle VARCHAR(50),
    exercise VARCHAR(50),
    repetition VARCHAR(50));

SELECT * FROM exercise_data;





CREATE TABLE elgaReport(
    elga_ID BIGSERIAL NOT NULL PRIMARY KEY);

SELECT * FROM elgaReport;



CREATE TABLE personalDATA(
    SV_Number VARCHAR (10) NOT NULL PRIMARY KEY,
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

SELECT * FROM medicalDATA;




CREATE TABLE mayCONTAIN(
    report_ID BIGINT REFERENCES report_result(report_ID),
    biodexreport_ID BIGINT REFERENCES biodex_report(biodexreport_ID),
    elga_ID BIGINT REFERENCES elgaReport(elga_ID),
    PRIMARY KEY (report_ID, biodexreport_ID, elga_ID));

SELECT * FROM mayCONTAIN;


CREATE TABLE givesInforamtion(
    elga_ID BIGINT REFERENCES elgaReport(elga_ID),
    SV_Number VARCHAR (10) REFERENCES personalDATA(SV_Number),
    medicalDATA_ID BIGINT REFERENCES medicalDATA(medicalDATA_ID),
    PRIMARY KEY (elga_ID, SV_Number, medicalDATA_ID));

SELECT * FROM givesInforamtion;


CREATE TABLE uses(
    biodexreport_ID BIGINT REFERENCES biodex_report(biodexreport_ID),
    exercise_ID BIGINT REFERENCES exercise_data(exercise_ID),
    settings_ID BIGINT REFERENCES settings(settings_ID),
    PRIMARY KEY (biodexreport_ID, exercise_ID, settings_ID));

SELECT * FROM uses;


