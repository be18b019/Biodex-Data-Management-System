-- ACHTUNG: DROP TABLE DATABASE WILL DELETE ALL RECORDS; FOR EVER; DONT USE IT

--DROP DATABASE IF EXISTS BiodexDB;
-- CREATE DATABASE BiodexDB;

\c BiodexDB;

-- DROP TABLE IF EXISTS uses;
-- DROP TABLE IF EXISTS givesInforamtion;
-- DROP TABLE IF EXISTS may_contain;
-- DROP TABLE IF EXISTS medical_data;
-- DROP TABLE IF EXISTS personal_data;
-- DROP TABLE IF EXISTS elga_report;
-- DROP TABLE IF EXISTS exercise_data;
-- DROP TABLE IF EXISTS settings;
-- DROP TABLE IF EXISTS biodex_report;
-- DROP TABLE IF EXISTS report_result;






CREATE TABLE report_result(
    report_id BIGSERIAL NOT NULL PRIMARY KEY,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP(0));  




CREATE TABLE biodex_report(
    biodex_report_id BIGSERIAL NOT NULL PRIMARY KEY);


CREATE TABLE settings(
    settings_id BIGSERIAL NOT NULL PRIMARY KEY,
    powerhead_orientation VARCHAR DEFAULT NULL,
    powerhead_height VARCHAR DEFAULT NULL,
    powerhead_position VARCHAR DEFAULT NULL,
    powerhead_attachment VARCHAR DEFAULT NULL,
    powerhead_tilt VARCHAR DEFAULT NULL,
    seat_height VARCHAR DEFAULT NULL,
    seat_orientation VARCHAR DEFAULT NULL,
    seat_tilt VARCHAR DEFAULT NULL,
    seat_position VARCHAR DEFAULT NULL,
    hip_flexion VARCHAR DEFAULT NULL,
    footplate_tilt VARCHAR DEFAULT NULL,
    knee_flexion VARCHAR DEFAULT NULL,
    ankle_flexion VARCHAR DEFAULT NULL,
    shoulder_abduction VARCHAR DEFAULT NULL,
    shoulder_flexion VARCHAR DEFAULT NULL,
    elbow_flexion VARCHAR DEFAULT NULL,
    controller_mode VARCHAR DEFAULT NULL,
    controller_cushion VARCHAR DEFAULT NULL,
    controller_sensitivity VARCHAR DEFAULT NULL,
    controller_rom_upper_limit VARCHAR DEFAULT NULL,
    controller_rom_lower_limit VARCHAR DEFAULT NULL,
    controller_rom_percentage VARCHAR DEFAULT NULL,
    controller_eccentric_speed VARCHAR DEFAULT NULL,
    controller_passive_speed VARCHAR DEFAULT NULL,
    controller_torque_limits VARCHAR DEFAULT NULL,
    controller_pause VARCHAR DEFAULT NULL,
    controller_isokinetic_speed VARCHAR DEFAULT NULL);

CREATE TABLE exercise_data(
    exercise_id BIGSERIAL NOT NULL PRIMARY KEY,
    torque VARCHAR DEFAULT NULL,
    angle VARCHAR DEFAULT NULL,
    velocity VARCHAR DEFAULT NULL,
    muscle VARCHAR DEFAULT NULL,
    exercise VARCHAR DEFAULT NULL,
    repetition VARCHAR DEFAULT NULL);



--left side of the ER-Diagram
CREATE TABLE elga_report(
    elga_report_id BIGSERIAL NOT NULL PRIMARY KEY);


CREATE TABLE personal_data(
    personal_data_id BIGSERIAL NOT NULL PRIMARY KEY,
    sv_number VARCHAR DEFAULT NULL,
    family_status VARCHAR DEFAULT NULL,
    email VARCHAR DEFAULT NULL,
    address VARCHAR DEFAULT NULL,
    legal_guardian VARCHAR DEFAULT NULL,
    religion VARCHAR DEFAULT NULL,
    language VARCHAR DEFAULT NULL,
    insurance VARCHAR DEFAULT NULL,
    telephone_number VARCHAR DEFAULT NULL,
    birth_place VARCHAR DEFAULT NULL,
    birth_date VARCHAR DEFAULT NULL,
    gender VARCHAR DEFAULT NULL,
    name_title VARCHAR DEFAULT NULL);

CREATE TABLE medical_data(
    medical_data_id BIGSERIAL NOT NULL PRIMARY KEY,
    hospital_start_date VARCHAR DEFAULT NULL,
    hospital_end_date VARCHAR DEFAULT NULL,
    hospital_address VARCHAR DEFAULT NULL,
    hospital_department VARCHAR DEFAULT NULL,
    hospital_admission_number VARCHAR DEFAULT NULL,
    hospital_name VARCHAR DEFAULT NULL,
    hospital_contact VARCHAR DEFAULT NULL,
    hospital_responsible_doctor VARCHAR DEFAULT NULL,
    diagnosis_state_at_release VARCHAR DEFAULT NULL,
    diagnosis_summary VARCHAR DEFAULT NULL,
    diagnosis_future_medication VARCHAR DEFAULT NULL,
    diagnosis_rehabilitation_aim VARCHAR DEFAULT NULL,
    diagnosis_recommended_measurements VARCHAR DEFAULT NULL,
    diagnosis_physical_issue VARCHAR DEFAULT NULL,

    medical_actions_by_hospital VARCHAR DEFAULT NULL,
    medical_medication_during_stay VARCHAR DEFAULT NULL,
    medical_medication_at_arrival VARCHAR DEFAULT NULL,
    medical_risk_allergies VARCHAR DEFAULT NULL,
    medical_previous_diseases VARCHAR DEFAULT NULL,
    medical_anamnesis VARCHAR DEFAULT NULL,
    medical_admission_reason VARCHAR DEFAULT NULL);




CREATE TABLE contains(
    report_id BIGINT REFERENCES report_result(report_id),
    biodex_report_id BIGINT REFERENCES biodex_report(biodex_report_id),
    elga_report_id BIGINT REFERENCES elga_report(elga_report_id),
    PRIMARY KEY (report_id, biodex_report_id, elga_report_id));

CREATE TABLE gives_information(
    elga_report_id BIGINT REFERENCES elga_report(elga_report_id),
    personal_data_id BIGINT REFERENCES personal_data(personal_data_id),
    medical_data_id BIGINT REFERENCES medical_data(medical_data_id),
    PRIMARY KEY (elga_report_id, personal_data_id, medical_data_id));

CREATE TABLE uses(
    biodex_report_id BIGINT REFERENCES biodex_report(biodex_report_id),
    exercise_id BIGINT REFERENCES exercise_data(exercise_id),
    settings_id BIGINT REFERENCES settings(settings_id),
    PRIMARY KEY (biodex_report_id, exercise_id, settings_id));









--- gut zu gebrauchen: ALTER SEQUENCE settings_settings_id_seq RESTART WITH 1
ALTER SEQUENCE biodex_report_biodex_report_id_seq RESTART WITH 1;
ALTER SEQUENCE elga_report_elga_id_seq RESTART WITH 1;
ALTER SEQUENCE exercise_data_exercise_id_seq RESTART WITH 1;
ALTER SEQUENCE medical_data_medical_data_id_seq RESTART WITH 1;
ALTER SEQUENCE personal_data_personal_data_id_seq RESTART WITH 1;
ALTER SEQUENCE report_result_report_id_seq RESTART WITH 1;
ALTER SEQUENCE settings_settings_id_seq RESTART WITH 1;














---------------------------------------------------------------------------------------FROM HERE THE FUNCTIONS WILL BE SCRIPTED

--is used to represent the DB in the Biodex Client
CREATE OR REPLACE FUNCTION display_table()
RETURNS TABLE
(
	id bigint,
	created_at timestamp,
	exercise character varying,
	muscle character varying,
	repetition character varying,
	name_title character varying
)
AS $$
BEGIN
	RETURN QUERY
	SELECT
		exercise_data.exercise_id,
		report_result.created_at,
		exercise_data.exercise,
		exercise_data.muscle,
		exercise_data.repetition,
		personal_data.name_title
	FROM 
		exercise_data
		INNER JOIN report_result ON report_result.report_id = exercise_data.exercise_id
		INNER JOIN personal_data ON report_result.report_id = personal_data.personal_data_id;
END $$ LANGUAGE plpgsql;


--is called when a certain exercise is choosen in the GUI to display
CREATE OR REPLACE FUNCTION display_selection(_selection TEXT)
RETURNS TABLE
(
	id bigint,
	created_at timestamp,
	exercise character varying,
	muscle character varying,
	repetition character varying,
	name_title character varying
)
AS $$
BEGIN
	RETURN QUERY
	SELECT
		exercise_data.exercise_id,
		report_result.created_at,
		exercise_data.exercise,
		exercise_data.muscle,
		exercise_data.repetition,
		personal_data.name_title
	FROM 
		exercise_data
		INNER JOIN report_result ON report_result.report_id = exercise_data.exercise_id
		INNER JOIN personal_data ON report_result.report_id = personal_data.personal_data_id
		WHERE exercise_data.exercise LIKE '%' || _selection || '%';
END $$ LANGUAGE plpgsql;





-------------------------------------------------- all "insert_" functions are used to take values from the text box of the Biodex Client (GUI) and insert them into the database --- and they return the last ID of the table
CREATE OR REPLACE FUNCTION insert_settings(
	_powerhead_orientation character varying,
    _powerhead_height character varying,
    _powerhead_position character varying,
    _powerhead_attachment character varying,
    _powerhead_tilt character varying,
    _seat_height character varying,
    _seat_orientation character varying,
    _seat_tilt character varying,
    _seat_position character varying,
    _hip_flexion character varying,
    _footplate_tilt character varying,
    _knee_flexion character varying,
    _ankle_flexion character varying,
    _shoulder_abduction character varying,
    _shoulder_flexion character varying,
    _elbow_flexion character varying,
    _controller_mode character varying,
    _controller_cushion character varying,
    _controller_sensitivity character varying,
    _controller_rom_upper_limit character varying,
    _controller_rom_lower_limit character varying,
    _controller_rom_percentage character varying,
    _controller_eccentric_speed character varying,
    _controller_passive_speed character varying,
    _controller_torque_limits character varying,
    _controller_pause character varying,
    _controller_isokinetic_speed character varying)
RETURNS int AS $$
BEGIN
	INSERT INTO settings(
	powerhead_orientation,
    powerhead_height,
    powerhead_position,
    powerhead_attachment,
    powerhead_tilt,
    seat_height,
    seat_orientation,
    seat_tilt,
    seat_position,
    hip_flexion,
    footplate_tilt,
    knee_flexion,
    ankle_flexion,
    shoulder_abduction,
    shoulder_flexion,
    elbow_flexion,
    controller_mode,
    controller_cushion,
    controller_sensitivity,
    controller_rom_upper_limit,
    controller_rom_lower_limit,
    controller_rom_percentage,
    controller_eccentric_speed,
    controller_passive_speed,
    controller_torque_limits,
    controller_pause,
    controller_isokinetic_speed)
	VALUES(
	_powerhead_orientation,
    _powerhead_height,
    _powerhead_position,
    _powerhead_attachment,
    _powerhead_tilt,
    _seat_height,
    _seat_orientation,
    _seat_tilt,
    _seat_position,
    _hip_flexion,
    _footplate_tilt,
    _knee_flexion,
    _ankle_flexion,
    _shoulder_abduction,
    _shoulder_flexion,
    _elbow_flexion,
    _controller_mode,
    _controller_cushion,
    _controller_sensitivity,
    _controller_rom_upper_limit,
    _controller_rom_lower_limit,
    _controller_rom_percentage,
    _controller_eccentric_speed,
    _controller_passive_speed,
    _controller_torque_limits,
    _controller_pause,
    _controller_isokinetic_speed);
	
	IF FOUND THEN RETURN (SELECT max(settings_id) FROM settings);
	ELSE RETURN 0;
	END IF;
	
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION insert_exercise_data(
    _torque character varying,
    _angle character varying,
    _velocity character varying,
    _muscle character varying,
    _exercise character varying,
    _repetition character varying)
RETURNS int AS $$
BEGIN
	INSERT INTO exercise_data(
    torque,
    angle,
    velocity,
    muscle,
    exercise,
    repetition)
	VALUES(
    _torque,
    _angle,
    _velocity,
    _muscle,
    _exercise,
    _repetition);
	
	IF FOUND THEN RETURN (SELECT max(exercise_id) FROM exercise_data);
	ELSE RETURN 0;
	END IF;
	
END $$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION insert_personal_data(
    _sv_number character varying,
    _family_status character varying,
    _email character varying,
    _address character varying,
    _legal_guardian character varying,
    _religion character varying,
    _language character varying,
    _insurance character varying,
    _telephone_number character varying,
    _birth_place character varying,
    _birth_date character varying,
    _gender character varying,
    _name_title character varying)
RETURNS int AS $$
BEGIN
	INSERT INTO personal_data(
    sv_number,
    family_status,
    email,
    address,
    legal_guardian,
    religion,
    language,
    insurance,
    telephone_number,
    birth_place,
    birth_date,
    gender,
    name_title)
	VALUES(
    _sv_number,
    _family_status,
    _email,
    _address,
    _legal_guardian,
    _religion,
    _language,
    _insurance,
    _telephone_number,
    _birth_place,
    _birth_date,
    _gender,
    _name_title);
	
	IF FOUND THEN RETURN (SELECT max(personal_data_id) FROM personal_data);
	ELSE RETURN 0;
	END IF;
	
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION insert_medical_data(
    _hospital_start_date character varying,
    _hospital_end_date character varying,
    _hospital_address character varying,
    _hospital_department character varying,
    _hospital_admission_number character varying,
    _hospital_name character varying,
    _hospital_contact character varying,
    _hospital_responsible_doctor character varying,
    _diagnosis_state_at_release character varying,
    _diagnosis_summary character varying,
    _diagnosis_future_medication character varying,
    _diagnosis_rehabilitation_aim character varying,
    _diagnosis_recommended_measurements character varying,
    _diagnosis_physical_issue character varying,
    _medical_actions_by_hospital character varying,
    _medical_medication_during_stay character varying,
    _medical_medication_at_arrival character varying,
    _medical_risk_allergies character varying,
    _medical_previous_diseases character varying,
    _medical_anamnesis character varying,
    _medical_admission_reason character varying)
RETURNS int AS $$
BEGIN
	INSERT INTO medical_data(
	hospital_start_date,
    hospital_end_date,
    hospital_address,
    hospital_department,
    hospital_admission_number,
    hospital_name,
    hospital_contact,
    hospital_responsible_doctor,
    diagnosis_state_at_release,
    diagnosis_summary,
    diagnosis_future_medication,
    diagnosis_rehabilitation_aim,
    diagnosis_recommended_measurements,
    diagnosis_physical_issue,
    medical_actions_by_hospital,
    medical_medication_during_stay,
    medical_medication_at_arrival,
    medical_risk_allergies,
    medical_previous_diseases,
    medical_anamnesis,
    medical_admission_reason)
	VALUES(
	_hospital_start_date,
    _hospital_end_date,
    _hospital_address,
    _hospital_department,
    _hospital_admission_number,
    _hospital_name,
    _hospital_contact,
    _hospital_responsible_doctor,
    _diagnosis_state_at_release,
    _diagnosis_summary,
    _diagnosis_future_medication,
    _diagnosis_rehabilitation_aim,
    _diagnosis_recommended_measurements,
    _diagnosis_physical_issue,
    _medical_actions_by_hospital,
    _medical_medication_during_stay,
    _medical_medication_at_arrival,
    _medical_risk_allergies,
    _medical_previous_diseases,
    _medical_anamnesis,
    _medical_admission_reason);
	
	IF FOUND THEN RETURN (SELECT max(medical_data_id) FROM medical_data);
	ELSE RETURN 0;
	END IF;
	
END $$ LANGUAGE plpgsql;



------------------------------------------------------------------------------ also used to create the tables, BUT: they dont need certain values, therfore they are filled with default values and retrun the last ID of the table
CREATE OR REPLACE FUNCTION insert_biodex_report()
RETURNS int AS $$
BEGIN
	INSERT INTO biodex_report DEFAULT VALUES;
	
	IF FOUND THEN RETURN (SELECT max(biodex_report_id) FROM biodex_report);
	ELSE RETURN 0;
	END IF;
	
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION insert_elga_report()
RETURNS int AS $$
BEGIN
	INSERT INTO elga_report DEFAULT VALUES;
	
	IF FOUND THEN RETURN (SELECT max(elga_report_id) FROM elga_report);
	ELSE RETURN 0;
	END IF;
	
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION insert_report_result()
RETURNS int AS $$
BEGIN
	INSERT INTO report_result DEFAULT VALUES;
	
	IF FOUND THEN RETURN (SELECT max(report_id) FROM report_result);
	ELSE RETURN 0;
	END IF;
	
END $$ LANGUAGE plpgsql;



-----------------------------------------------------------------------are used to create the connections (Relation) tables between the each grand tables, they will take their parameter from the BIODEX CLIENT GUI
CREATE OR REPLACE FUNCTION insert_uses(
	_biodex_report_id INT,
	_exercise_id INT,
	_settings_id INT)
RETURNS int AS $$
BEGIN
	INSERT INTO uses (biodex_report_id, exercise_id, settings_id)
	VALUES (_biodex_report_id, _exercise_id, _settings_id);
	
	IF FOUND THEN RETURN 1;
	ELSE RETURN 0;
	END IF;
	
END $$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION insert_gives_information(
	_elga_report_id INT,
	_personal_data_id INT,
	_medical_data_id INT)
RETURNS int AS $$
BEGIN
	INSERT INTO gives_information (elga_report_id, personal_data_id, medical_data_id)
	VALUES (_elga_report_id, _personal_data_id, _medical_data_id);
	
	IF FOUND THEN RETURN 1;
	ELSE RETURN 0;
	END IF;
	
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION insert_contains(
	_report_id INT,
	_biodex_report_id INT,
	_elga_report_id INT)
RETURNS int AS $$
BEGIN
	INSERT INTO contains (report_id, biodex_report_id, elga_report_id)
	VALUES (_report_id, _biodex_report_id, _elga_report_id);
	
	IF FOUND THEN RETURN 1;
	ELSE RETURN 0;
	END IF;
	
END $$ LANGUAGE plpgsql;



--------------------------------------------------------------------------------is used to get the torque, angle and velocity from the Database and load it in the GUI (LOAD BUTTON)
CREATE OR REPLACE FUNCTION return_torque(_id INT)
RETURNS character varying
AS $$
BEGIN
	RETURN (SELECT torque FROM exercise_data WHERE exercise_id = _id);
END $$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION return_angle(_id INT)
RETURNS character varying
AS $$
BEGIN
	RETURN (SELECT angle FROM exercise_data WHERE exercise_id = _id);
END $$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION return_velocity(_id INT)
RETURNS character varying
AS $$
BEGIN
	RETURN (SELECT velocity FROM exercise_data WHERE exercise_id = _id);
END $$ LANGUAGE plpgsql;



---------------------------------------------------------------------------------is needed to call the values from the DB to insert them into the CSV
CREATE OR REPLACE FUNCTION get_porientation(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT powerhead_orientation FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;




CREATE OR REPLACE FUNCTION get_powerhead_height(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT powerhead_height FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_powerhead_position(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT powerhead_position FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_powerhead_attachment(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT powerhead_attachment FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_powerhead_tilt(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT powerhead_tilt FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;






CREATE OR REPLACE FUNCTION get_seat_height(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT seat_height FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_seat_orientation(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT seat_orientation FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_seat_tilt(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT seat_tilt FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_seat_position(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT seat_position FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;






CREATE OR REPLACE FUNCTION get_controller_mode(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT controller_mode FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_controller_cushion(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT controller_cushion FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_controller_sensitivity(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT controller_sensitivity FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_controller_rom_upper_limit(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT controller_rom_upper_limit FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_controller_rom_lower_limit(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT controller_rom_lower_limit FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_controller_rom_percentage(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT controller_rom_percentage FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_controller_eccentric_speed(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT controller_eccentric_speed FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_controller_passive_speed(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT controller_passive_speed FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_controller_torque_limits(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT controller_torque_limits FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_controller_pause(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT controller_pause FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_controller_isokinetic_speed(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT controller_isokinetic_speed FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;







CREATE OR REPLACE FUNCTION get_hip_flexion(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT hip_flexion FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_footplate_tilt(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT footplate_tilt FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_knee_flexion(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT knee_flexion FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_ankle_flexion(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT ankle_flexion FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_shoulder_abduction(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT shoulder_abduction FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_shoulder_flexion(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT shoulder_flexion FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_elbow_flexion(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT elbow_flexion FROM settings WHERE settings_id = _id);
END $$ LANGUAGE plpgsql;


--------------------- get the second part of the csv: MEDICAL DATA PART

---personal
CREATE OR REPLACE FUNCTION get_sv_number(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT sv_number FROM personal_data WHERE personal_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_family_status(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT family_status FROM personal_data WHERE personal_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_email(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT email FROM personal_data WHERE personal_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_address(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT address FROM personal_data WHERE personal_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_legal_guardian(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT legal_guardian FROM personal_data WHERE personal_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_religion(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT religion FROM personal_data WHERE personal_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_language(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT language FROM personal_data WHERE personal_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_insurance(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT insurance FROM personal_data WHERE personal_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_telephone_number(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT telephone_number FROM personal_data WHERE personal_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_birth_place(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT birth_place FROM personal_data WHERE personal_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_birth_date(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT birth_date FROM personal_data WHERE personal_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_gender(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT gender FROM personal_data WHERE personal_data_id = _id);
END $$ LANGUAGE plpgsql;




---hospital
CREATE OR REPLACE FUNCTION get_hospital_start_date(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT hospital_start_date FROM medical_data WHERE medical_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_hospital_end_date(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT hospital_end_date FROM medical_data WHERE medical_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_hospital_address(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT hospital_address FROM medical_data WHERE medical_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_hospital_department(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT hospital_department FROM medical_data WHERE medical_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_hospital_admission_number(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT hospital_admission_number FROM medical_data WHERE medical_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_hospital_name(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT hospital_name FROM medical_data WHERE medical_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_hospital_contact(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT hospital_contact FROM medical_data WHERE medical_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_hospital_responsible_doctor(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT hospital_responsible_doctor FROM medical_data WHERE medical_data_id = _id);
END $$ LANGUAGE plpgsql;




--- diagnosis
CREATE OR REPLACE FUNCTION get_diagnosis_state_at_release(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT diagnosis_state_at_release FROM medical_data WHERE medical_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_diagnosis_summary(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT diagnosis_summary FROM medical_data WHERE medical_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_diagnosis_future_medication(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT diagnosis_future_medication FROM medical_data WHERE medical_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_diagnosis_rehabilitation_aim(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT diagnosis_rehabilitation_aim FROM medical_data WHERE medical_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_diagnosis_recommended_measurements(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT diagnosis_recommended_measurements FROM medical_data WHERE medical_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_diagnosis_physical_issue(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT diagnosis_physical_issue FROM medical_data WHERE medical_data_id = _id);
END $$ LANGUAGE plpgsql;



--- medical

CREATE OR REPLACE FUNCTION get_medical_actions_by_hospital(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT medical_actions_by_hospital FROM medical_data WHERE medical_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_medical_medication_during_stay(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT medical_medication_during_stay FROM medical_data WHERE medical_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_medical_medication_at_arrival(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT medical_medication_at_arrival FROM medical_data WHERE medical_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_medical_risk_allergies(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT medical_risk_allergies FROM medical_data WHERE medical_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_medical_previous_diseases(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT medical_previous_diseases FROM medical_data WHERE medical_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_medical_anamnesis(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT medical_anamnesis FROM medical_data WHERE medical_data_id = _id);
END $$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_medical_admission_reason(_id INT)
RETURNS character varying AS $$
BEGIN
	RETURN (SELECT medical_admission_reason FROM medical_data WHERE medical_data_id = _id);
END $$ LANGUAGE plpgsql;




----------------------------------------------------------------------------------will be used in emergency cases, when the IDs from each grand table do not match
ALTER SEQUENCE report_result_report_id_seq RESTART WITH 1;
ALTER SEQUENCE biodex_report_biodex_report_id_seq RESTART WITH 1;
ALTER SEQUENCE elga_report_elga_report_id_seq RESTART WITH 1;
ALTER SEQUENCE exercise_data_exercise_id_seq RESTART WITH 1;
ALTER SEQUENCE settings_settings_id_seq RESTART WITH 1;
ALTER SEQUENCE medical_data_medical_data_id_seq RESTART WITH 1;
ALTER SEQUENCE personal_data_personal_data_id_seq RESTART WITH 1;

--------------------------------------------------------------------------------------useful commands
DELETE FROM contains;
DELETE FROM uses;
DELETE FROM gives_information;
DELETE FROM report_result;
DELETE FROM biodex_report;
DELETE FROM elga_report;
DELETE FROM settings;
DELETE FROM exercise_data;
DELETE FROM personal_data;
DELETE FROM medical_data;