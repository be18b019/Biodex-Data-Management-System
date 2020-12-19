using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biodex_Client.DB_Classes;

namespace Biodex_Client
{
    class DataAccessObject
    {
        private readonly string connString = "Host=localhost;Username=postgres;Password=Password123;Database=BiodexDB";

        public long insertIntoSettings(Settings settings)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connString);
            conn.OpenAsync();
            using (var cmd = new NpgsqlCommand("INSERT INTO settings (powerhead_orientation, powerhead_height, powerhead_position, powerhead_attachment, powerhead_tilt, seat_height, seat_orientation, seat_tilt, seat_position, hip_flexion, footplate_tilt, knee_flexion, ankle_flexion, shoulder_abduction, shoulder_flexion, elbow_flexion, controller_mode, controller_cushion, controller_sensitivity, controller_rom_upper_limit,controller_rom_lower_limit, controller_rom_percentage, controller_eccentric_speed, controller_passive_speed, controller_torque_limits, controller_pause, controller_isokinetic_speed) VALUES (@po,@ph,@pp,@pa,@pt,@sh,@so,@st,@sp,@hf,@ft,@kf,@af,@sa,@sf,@ef,@cm,@cc,@cs,@crul,@crll,@crp,@ces,@cps,@ctl,@cp,@cis) RETURNING settings_ID", conn))
            {
                cmd.Parameters.AddWithValue("po", settings.Powerhead_ORIENTATION);
                cmd.Parameters.AddWithValue("ph", settings.Powerhead_HEIGHT);
                cmd.Parameters.AddWithValue("pp", settings.Powerhead_POSITION);
                cmd.Parameters.AddWithValue("pa", settings.Powerhead_ATTACHMENT);
                cmd.Parameters.AddWithValue("pt", settings.Powerhead_TILT);
                cmd.Parameters.AddWithValue("st", settings.Seat_HEIGHT);
                cmd.Parameters.AddWithValue("so", settings.Seat_ORIENTATION);
                cmd.Parameters.AddWithValue("st", settings.Seat_TILT);
                cmd.Parameters.AddWithValue("sp", settings.Seat_POSITION);
                cmd.Parameters.AddWithValue("hf", settings.Hip_FLEXION);
                cmd.Parameters.AddWithValue("ft", settings.Footplate_TILT);
                cmd.Parameters.AddWithValue("kf", settings.Knee_FLEXION);
                cmd.Parameters.AddWithValue("af", settings.Ankle_FLEXION);
                cmd.Parameters.AddWithValue("sa", settings.Shoulder_ABDUCTION);
                cmd.Parameters.AddWithValue("sf", settings.Shoulder_FLEXION);
                cmd.Parameters.AddWithValue("cm", settings.Controller_MODE);
                cmd.Parameters.AddWithValue("cc", settings.Controller_CUSHION);
                cmd.Parameters.AddWithValue("cs", settings.Controller_SENSITIVITY);
                cmd.Parameters.AddWithValue("crul", settings.Controller_ROM_UPPER_LIMIT);
                cmd.Parameters.AddWithValue("crll", settings.Controller_ROM_LOWER_LIMIT);
                cmd.Parameters.AddWithValue("crp", settings.Controller_ROM_PERCENTAGE);
                cmd.Parameters.AddWithValue("ces", settings.Controller_ECCENTRIC_SPEED);
                cmd.Parameters.AddWithValue("cps", settings.Controller_PASSIVE_SPEED);
                cmd.Parameters.AddWithValue("ctl", settings.Controller_TOURQUE_LIMITS);
                cmd.Parameters.AddWithValue("cp", settings.Controller_PAUSE);
                cmd.Parameters.AddWithValue("cis", settings.Controller_ISOKINETIC_SPEED);
                cmd.Parameters.AddWithValue("ef", settings.ELBOW_FLEXION);
                Object res =cmd.ExecuteScalar();
                return long.Parse(res.ToString());
            }
        }

        public long insertIntoMedicalDataAsync(medicalData medicaldata)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connString);
            conn.OpenAsync();

            // Insert some data
            //await using (var cmd = new NpgsqlCommand("INSERT INTO data (some_field) VALUES (@p)", conn))
            using (var cmd = new NpgsqlCommand("INSERT INTO medicaldata (hospitalstartdate, hospitalenddate, hospitaladress, hospitaldepartment, hospitaladmissionnumber, hospitalname, hospitalcontact, hospitalresponsibledoctor, diagstateatrelease, diagsummary, diagfuturemedication, diagrehaaim, diagrecommendedmeasurement, diagphysicalissue, medactionsbyhospital, medmedicationduringstay, medmedicationatarrival, medriskalergies, medpreviousdisease, medanamnesis, medadmissionreason) VALUES (@hsd,@hed,@ha,@hd,@han,@hn,@hc,@hrd,@dsr,@ds,@dfm,@dr,@drm,@dpi,@mah,@mms,@mma,@mra,@mpd,@ma,@mar) RETURNING medicalDataID", conn))
            {
                cmd.Parameters.AddWithValue("hsd", medicaldata.hospitalStartDate);
                cmd.Parameters.AddWithValue("hed", medicaldata.hospitalEndDate);
                cmd.Parameters.AddWithValue("ha", medicaldata.hospitalAdress);
                cmd.Parameters.AddWithValue("hd", medicaldata.hospitalDepartment);
                cmd.Parameters.AddWithValue("han", medicaldata.hospitalAdmissionNumber);
                cmd.Parameters.AddWithValue("hn", medicaldata.hospitalName);
                cmd.Parameters.AddWithValue("hc", medicaldata.hospitalContact);
                cmd.Parameters.AddWithValue("hrd", medicaldata.hospitalResposibleDoctor);
                cmd.Parameters.AddWithValue("dsr", medicaldata.diagStateAtRelease);
                cmd.Parameters.AddWithValue("ds", medicaldata.diagSummary);
                cmd.Parameters.AddWithValue("dfm", medicaldata.diagFutureMedication);
                cmd.Parameters.AddWithValue("dr", medicaldata.diagRehaAim);
                cmd.Parameters.AddWithValue("drm", medicaldata.diagRecommendedMeasurement);
                cmd.Parameters.AddWithValue("dpi", medicaldata.diagPhysicalIssue);
                cmd.Parameters.AddWithValue("mah", medicaldata.medActionsByHospital);
                cmd.Parameters.AddWithValue("mms", medicaldata.medMedicationDurringStay);
                cmd.Parameters.AddWithValue("mma", medicaldata.medMedicationAtArrival);
                cmd.Parameters.AddWithValue("mra", medicaldata.medRiskAlergies);
                cmd.Parameters.AddWithValue("mpd", medicaldata.medPreviousDisease);
                cmd.Parameters.AddWithValue("ma", medicaldata.medAnamnesis);
                cmd.Parameters.AddWithValue("mar", medicaldata.medAdmissionReason);
                Object res = cmd.ExecuteScalar();
                return long.Parse(res.ToString());
            }
        }

        public async Task insertIntoBiodexReportAsync(BiodexReport biodexReport)
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            // Insert some data
            //await using (var cmd = new NpgsqlCommand("INSERT INTO data (some_field) VALUES (@p)", conn))
            using (var cmd = new NpgsqlCommand("INSERT INTO biodex_report (Torque, Angle, AngleVelocity, Exercise, Muscle, Repetition, settings_ID) VALUES (@t,@a,@av,@e,@m,@r,@si)", conn))
            {
                cmd.Parameters.AddWithValue("t", biodexReport.Torque);
                cmd.Parameters.AddWithValue("a", biodexReport.Angle);
                cmd.Parameters.AddWithValue("av", biodexReport.AngleVelocity);
                cmd.Parameters.AddWithValue("e", biodexReport.Exercise);
                cmd.Parameters.AddWithValue("m", biodexReport.Muscle);
                cmd.Parameters.AddWithValue("r", biodexReport.Repetition);
                cmd.Parameters.AddWithValue("si", biodexReport.SettingsID);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public string insertIntoPersonalDataAsync(personalData personaldata)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connString);
            conn.OpenAsync();

            // Insert some data
            //await using (var cmd = new NpgsqlCommand("INSERT INTO data (some_field) VALUES (@p)", conn))
            using (var cmd = new NpgsqlCommand("INSERT INTO biodex_report (sV_Number, familyStatus, email, adress, legalGuardian, religion, language, insurance, telNumber, birthPlace, birthDATE, gender, nameTitel) VALUES (@fs,@e,@ad,@lg,@r,@l,@i,@tm,@bp,@bd,@g,@nt) RETURNING SV_Number", conn))
            {
                cmd.Parameters.AddWithValue("fs", personaldata.familyStatus);
                cmd.Parameters.AddWithValue("e", personaldata.Email);
                cmd.Parameters.AddWithValue("ad", personaldata.adress);
                cmd.Parameters.AddWithValue("lg", personaldata.legalGuardian);
                cmd.Parameters.AddWithValue("r", personaldata.religion);
                cmd.Parameters.AddWithValue("l", personaldata.laguage);
                cmd.Parameters.AddWithValue("i", personaldata.insurance);
                cmd.Parameters.AddWithValue("tm", personaldata.telNumber);
                cmd.Parameters.AddWithValue("bp", personaldata.birthPlace);
                cmd.Parameters.AddWithValue("bd", personaldata.birthDATE);
                cmd.Parameters.AddWithValue("g", personaldata.gender);
                cmd.Parameters.AddWithValue("nt", personaldata.NameTitel);
                Object res = cmd.ExecuteScalar();
                return res.ToString();
            }
        }

        public long insertIntoElgaReport(elgaReport elgareport)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connString);
            conn.OpenAsync();

            // Insert some data
            //await using (var cmd = new NpgsqlCommand("INSERT INTO data (some_field) VALUES (@p)", conn))
            using (var cmd = new NpgsqlCommand("INSERT INTO elgareport DEFAULT VALUES RETURNING elga_ID", conn))
            {
                Object res = cmd.ExecuteScalar();
                return long.Parse(res.ToString());
            }
        }

        public async Task insertIntoProband(Proband proband)
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            // Insert some data
            //await using (var cmd = new NpgsqlCommand("INSERT INTO data (some_field) VALUES (@p)", conn))
            using (var cmd = new NpgsqlCommand("INSERT INTO Proband(proband_name, matriculation_number) VALUES (@n,@m)", conn))
            {
                cmd.Parameters.AddWithValue("n", proband.probandName);
                cmd.Parameters.AddWithValue("m", proband.MatriculationNumber);
                await cmd.ExecuteNonQueryAsync();
            }
        }

    }
}
