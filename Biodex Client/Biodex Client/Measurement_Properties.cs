using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using System.Threading;
using Biodex_Client.DB_Classes;
using Npgsql;
using CsvHelper;

namespace Biodex_Client
{

    public partial class formMeasurementProperties : Form
    {
        #region initializing form members and Chartvalues
        formGraphs _FormGraphs = null;
        Data _data = null;
        SerialPortSave serialportsave = null;
        //MProperties _mProperties = null;
        //PatientData _patientData = null;

        //public ChartValues<ValuePoint> ChartValuesTorqueValues { get; set; }
        //public ChartValues<ValuePoint> ChartValuesVelocityValues { get; set; }
        //public ChartValues<ValuePoint> ChartValuesAngleValues { get; set; }

        //Thread threadAddValuesToChart;
        #endregion

        #region initializing Database objects and variables
        //tips from: https://www.youtube.com/watch?v=U_v1dSglNjE

        private NpgsqlConnection conn;
        private string connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
            "localhost", 5432, "postgres", "Password123", "BiodexDB");

        private string sql;             //this is a teporary string to hand over the sql commands
        private NpgsqlCommand cmd;      //represents a function or a statement - this object will excute a command against the database
        private DataTable dt;           //will only create a table to diplay it later on the GUI Available Measurement Table

        private int rowIndex = -1;      //is used to work with the CellClick() method ... "-1" means, nothing is selected

        //strings to fill in and send TO the DB
        string torqueStringToDB = null;
        string angleStringToDB = null;
        string velocityStringToDB = null;

        //string will be filled with values from the database
        string torqueStringFromDB = null;
        string angleStringFromDB = null;
        string velocityStringFromDB = null;

        #endregion

        #region formMeasurement Constructors, initializing of some plotting stuff
        public formMeasurementProperties()
        {
            InitializeComponent();
        }

        /*
         * custom constructor for initializing serveral objects
         */
        public formMeasurementProperties(formGraphs FormGraphs, Data data, SerialPortSave serialportsave)
        {
            _FormGraphs = FormGraphs;
            _data = data;
            this.serialportsave = serialportsave;

            //var mapper = Mappers.Xy<ValuePoint>()
            //     .X(model => model.Frame)
            //     .Y(model => model.Value);

            //Charting.For<ValuePoint>(mapper);

            //ChartValuesTorqueValues = new ChartValues<ValuePoint>();
            //ChartValuesVelocityValues = new ChartValues<ValuePoint>();
            //ChartValuesAngleValues = new ChartValues<ValuePoint>();


            ////chartTorqueInitialisation
            //_FormGraphs.chartTorque.DisableAnimations = true;
            //_FormGraphs.chartTorque.AxisX.Add(new LiveCharts.Wpf.Axis
            //{
            //    Title = "Frames",
            //    //DisableAnimations = true,
            //    //MaxValue = graphData.Time.Length
            //});

            //_FormGraphs.chartTorque.AxisY.Add(new LiveCharts.Wpf.Axis
            //{
            //    Title = "Torque",
            //    //DisableAnimations = true,
            //    //MaxValue = 600, 
            //    //MinValue=400
            //});



            ////chartVelocityInitialisation
            //_FormGraphs.chartVelocity.DisableAnimations = true;
            //_FormGraphs.chartVelocity.AxisX.Add(new LiveCharts.Wpf.Axis
            //{
            //    Title = "Frames",
            //    //DisableAnimations = true,
            //    //MaxValue = graphData.Time.Length
            //});

            //_FormGraphs.chartVelocity.AxisY.Add(new LiveCharts.Wpf.Axis
            //{
            //    Title = "Angular Velocity",
            //    //DisableAnimations = true,
            //    //MaxValue = 600, 
            //    //MinValue=400
            //});



            ////chartAngleInitialisation
            //_FormGraphs.chartAngle.DisableAnimations = true;
            //_FormGraphs.chartAngle.AxisX.Add(new LiveCharts.Wpf.Axis
            //{
            //    Title = "Frames",
            //    //DisableAnimations = true,
            //    //MaxValue = graphData.Time.Length
            //});

            //_FormGraphs.chartAngle.AxisY.Add(new LiveCharts.Wpf.Axis
            //{
            //    Title = "Angle",
            //    //DisableAnimations = true,
            //    //MaxValue = 600, 
            //    //MinValue=400
            //});

            InitializeComponent();
        }
        #endregion

        #region initialization of some GUI stuff
        /*
       * added getControls function to:
       * https://stackoverflow.com/questions/59862561/how-to-disable-scrolling-on-a-numericupdown-in-a-windows-form
       * adds an Eventhandler to MousewheelEvents for disabling scrolling
       */
        private void formMeasurementProperties_Load(object sender, EventArgs e)
        {

            //DATABASE REGION
            #region first connection to the DB and display_table()

            //connection to the database will be built, as the window opens
            conn = new NpgsqlConnection(connstring);        //with this command the connection will be built

            display_table();            //as the window opens the display_table() function will be called simultaneously

            #endregion


            List<Control> Ctrls = getControls();
            foreach (Control ctl in Ctrls)
            {
                if (ctl.GetType() == typeof(NumericUpDown))
                    ctl.MouseWheel += Ctl_MouseWheel;
            }
        }

        /*
         * sets handled eventarg true to disable scrolling
         */
        private void Ctl_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }

        /*
         * searches for controls of type NumericUpDown in all containers 
         * @return returns a list of all controls in order to provide access if maybe needed later
         */
        private List<Control> getControls()
        {
            List<Control> cntrls = new List<Control>();
            cntrls.Clear();

            foreach (TableLayoutPanel tlp in panelMain.Controls)
            {
                foreach (GroupBox gb in tlp.Controls)
                {
                    foreach (TableLayoutPanel tlp1 in gb.Controls)
                    {
                        foreach (TableLayoutPanel tlp2 in tlp1.Controls)
                        {
                            foreach (Control c in tlp2.Controls)
                            {
                                cntrls.Add(c);
                            }
                        }

                    }
                }
            }
            return cntrls;
        }
        #endregion

        #region deleting collected data with clear buttonclick
        /*
         * resets all controls of measurement properties and sets _data, _mProperties and _patientData to null
         */
        private void btnClear_Click(object sender, EventArgs e)
        {
            List<Control> Ctrls = getControls();
            foreach (Control ctl in Ctrls)
            {
                if (ctl.GetType() == typeof(TextBox))
                {
                    ctl.Text = null;
                }
                if (ctl.GetType() == typeof(ComboBox))
                {
                    ComboBox comboBox = (ComboBox)ctl;
                    comboBox.SelectedIndex = -1;
                }
                if (ctl.GetType() == typeof(NumericUpDown))
                {
                    NumericUpDown numericUpDown = (NumericUpDown)ctl;
                    numericUpDown.Value = 0;
                }

                _data = null;
                serialportsave.myData = null;
                //_mProperties = null;
                //_patientData = null;
            }

            //resetting the selection in the Datagrid View (Database Table)
            rowIndex = -1;
            dgvAMmeasurements.ClearSelection();

            //resetting all strings
            torqueStringToDB = null;
            angleStringToDB = null;
            velocityStringToDB = null;
            torqueStringFromDB = null;
            angleStringFromDB = null;
            velocityStringFromDB = null;

            MessageBox.Show("ALL VALUES HAVE BEEN RESET", "VALUES TO NULL", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region insert Dummy Data into the text boxes from the Patient Data Section
        private void btnPDSSimulatePatientData_Click(object sender, EventArgs e)
        {
            //generating the dummy data according to the ELGA Entlassungsbrief
            string DummyData;
            string[] seperator = { "|$|" };

            //depending on the choice from the dropdown cbxPDSChoosePatient a dummy dataset will be displayed
            if (cbxPDSChoosePatient.Text == "Patient 1")
            {
                DummyData = "Dipl.Ing. Hofrat Mustermann, BSc, MBA|$|1111241261|$|Männlich|$|24. Dezember 1961|$|Graz|$|+43 2682 40400|$|Wiener Gebietskrankenkasse|$|Deutsch (spricht: ausgezeichnet), bevorzugt|$|Römisch-Katholisch|$|Susi Sorgenvoll|$|Musterstraße 13a 7000 Eisenstadt, Burgenland|$|herberthannes.mustermann@provider.at|$|Vater|$|Amadeus Spital|$|Chirurgische Abteilung|$|Mozartgasse 1-7 5350 St.Wolfgang, Salzburg|$|+43 6138 3453446 0, info@amadeusspital.at|$|30. Juli 2016 um 10:46|$|17. August 2016 um 12:00 Uhr|$|Az123456|$|Univ.-Prof. Dr. Sigrid Kollmann|$|Gelenksempyem im linken Knie|$|Sturz bei sportlicher Betätigung|$|Trichterbrustoperation in der Jugend|$|Penicillin|$|Nasirin 0,05 %, Ciproxin 400 mg|$|Augmentin 2g, Ciproxin 400mg, Fosfomycin 2g|$|Antimikrobielle Therapie|$|Patient mobil eingeschränkt|$|rezidivierende Rückenschmerzen|$|Schonung, absolutes Sportverbot|$|Mobilität/Stab ges. WS, Bodentransfer|$|Diazepam, Zithromax, Nasivin|$|Revisions-OP, antimikrobielle Therapie|$||$||$|";
            }
            else if (cbxPDSChoosePatient.Text == "Patient 2")
            {
                DummyData = "Dr. Donald Duck|$|0123456789|$|male duck|$|24. January 1944|$|California|$|+1 2682 40400|$|World Disney|$| English (speaks: excellent), preferred |$|no specification|$|Daisy Duck (Wife)|$|Duckburg 13a 7000 , Calisota|$|donald.duck@disney.at|$|Dad and Husband|$|Charité - Universitätsmedizin Berlin (Berlin, Germany)|$|Surgical Department|$| 9500 Euclid Ave. Cleveland , Ohio 44195 |$| +1 216-444-2200, info@clevelandclinic.at|$|14. February 2008 at 10:46|$|5. August 2008 at 12:00|$|WDDD123456|$|Univ.-Prof. Dr. Martin Arrowsmith |$| joint empyema in the left knee |$| Fall during sporting activity |$| Funnel chest surgery in youth |$|Methicillin|$| Bactrim 0,1 %, Welchol 700 mg|$|Tadalafil 7g, Purelax 400mg, Fosfomycin 2g|$| Antimicrobial therapy|$| Patient with limited mobility |$|recurrent back pain |$| Rest, absolute ban on sport |$|Restoring Mobility|$| Fleet Bisacodyl, Senna S, Docuzen |$| Revisions-OP, Antimicrobial therapy|$||$||$|";
            }
            else if (cbxPDSChoosePatient.Text == "Patient 3")
            {
                DummyData = "Peter Parker|$|9876543210|$|male|$|26. March 1998|$|Vienna|$|+1 0676 133|$|Daily Bugle|$| French (speaks: excellent), preferred, also German |$|peace and hapiness|$|May Parker (Aunt)|$|20 Ingram Street in Forest Hills, Queens|$|spiderman@avengers.at|$|single|$|Massachusetts General Hospital (Boston)|$|Radiology Department|$| 55 Fruit St, Boston, MA 02114 |$| +1 617-726-2000, info@bostonclinic.at|$|8. October 2009 at 10:46|$|5. August 2010 at 12:00|$|MASM987456|$|Univ.-Prof. Dr. Curtis Connors|$| nullum venenum aranea |$| bitten by a radioactive spider |$| torn ligaments of the legs in youth |$|Mefenamic acid|$| Bactrim 0,1 %, Welchol 700 mg|$|Tadalafil 7g, Purelax 400mg, Fosfomycin 2g|$|monoclonal antibody therapy|$| Patient with weaked immune system and low stamina |$|recurrent ligament|$| Rest, absolute ban on sport |$|Gaining back strength|$| Fleet Bisacodyl, Senna S, Docuzen |$| water cure (therapy), neutron therapy |$||$||$|";
            }
            else
            {
                MessageBox.Show("Please select a patient.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
                //DummyData = "Name Titel|$|SV-Number|$|Gender|$|Birth Date|$|Birth Place|$|Phone Number|$|Insurance|$|Language|$|Religion|$|Guardian|$|Adress|$|Email|$|Family Status|$|Hospital Name|$|Hospital Department|$|Hospital Adress|$|Hospital Contact|$|Start Date|$|End Date|$|Admission Number|$|Responsible Doctor|$|Admission Reason|$|Anamnesis|$|Previous Diseases|$|Risk and Allergies|$|Medication At Arrival|$|Medication During Stay|$|Actions By Hospital|$|State At Release|$|Pysical Issue|$|Recommended Measurements|$|Rehabilitation Aim|$|Future Medication|$|Diagnosis Summary|$||$||$|";
            }

            string[] DummyDataArray = DummyData.Split(seperator, StringSplitOptions.None);

            //inserting the values from the String Array Dummy Data into each Text Box
            int i = 0;
            txtbPDTitleName.Text = DummyDataArray[i++];
            txtbPDSVNumber.Text = DummyDataArray[i++];
            txtbPDGender.Text = DummyDataArray[i++];
            txtbPDDateOfBirth.Text = DummyDataArray[i++];
            txtbPDPlaceOfBirth.Text = DummyDataArray[i++];
            txtbPDPhoneNumber.Text = DummyDataArray[i++];
            txtbPDInsurance.Text = DummyDataArray[i++];
            txtbPDLanguage.Text = DummyDataArray[i++];
            txtbPDReligion.Text = DummyDataArray[i++];
            txtbPDLegalGuardian.Text = DummyDataArray[i++];
            txtbPDAdress.Text = DummyDataArray[i++];
            txtbPDEmail.Text = DummyDataArray[i++];
            txtbPDFamilyStatus.Text = DummyDataArray[i++];
            txtbHIHospitalName.Text = DummyDataArray[i++];
            txtbHIDepartment.Text = DummyDataArray[i++];
            txtbHIHospitalAdress.Text = DummyDataArray[i++];
            txtbHIHospitalConatct.Text = DummyDataArray[i++];
            txtbHIStartDate.Text = DummyDataArray[i++];
            txtbHIEndDate.Text = DummyDataArray[i++];
            txtbHIAdmissionNumber.Text = DummyDataArray[i++];
            txtbHIResponsibleDoctor.Text = DummyDataArray[i++];
            txtbMDAdmissionReason.Text = DummyDataArray[i++];
            txtbMDAnamnesis.Text = DummyDataArray[i++];
            txtbMDPreviousDisease.Text = DummyDataArray[i++];
            txtbMDRisksAllergies.Text = DummyDataArray[i++];
            txtbMDMedicationArrival.Text = DummyDataArray[i++];
            txtbMDMedicationStay.Text = DummyDataArray[i++];
            txtbMDActionsHospital.Text = DummyDataArray[i++];
            txtbDStateRelease.Text = DummyDataArray[i++];
            txtbDPhysicalIssue.Text = DummyDataArray[i++];
            txtbDRecommendedMeasuremnts.Text = DummyDataArray[i++];
            txtbDRehabilitationAim.Text = DummyDataArray[i++];
            txtbDFutureMedication.Text = DummyDataArray[i++];
            txtbDSummary.Text = DummyDataArray[i++];
            MessageBox.Show("Patient data was entered in the fields below.", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region required methods to interact with the table in the GUI --- display_table(), dgvAMmeasurements_CellClick(), cbxAMChooseExercise_SelectedIndexChanged()

        //connect to the DB and displays the current (predefined) values from the DB in the DataGridView
        private void display_table()
        {
            if (cbxAMChooseExercise.Text == "Show All Records" || cbxAMChooseExercise.SelectedIndex == -1)
            {
                try
                {
                    conn.Open();

                    sql = @"SELECT * FROM display_table();";         //copy the desired function (here: SELECT, JOIN) into this string - TIP: try it out on Admin beforehand
                    cmd = new NpgsqlCommand(sql, conn);
                    dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());               //fills the DataTable with data from the source, takes everything, also naming from postgres

                    conn.Close();

                    dgvAMmeasurements.DataSource = null;                  //reset datagridview
                    dgvAMmeasurements.DataSource = dt;                    //connects to the source from which the data should be displayed
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                try
                {
                    conn.Open();

                    string selected = cbxAMChooseExercise.Text;

                    sql = @"SELECT * FROM display_selection(:_selection);";         //copy the desired function into this string - TIP: try it out on Admin beforehand
                    cmd = new NpgsqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("_selection", selected);

                    dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());               //fills the DataTable with data from the source, takes everything, also naming from postgres

                    conn.Close();

                    dgvAMmeasurements.DataSource = null;                  //reset datagridview
                    dgvAMmeasurements.DataSource = dt;                    //connects to the source from which the data should be displayed
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        //when item from combobox is choosen, the datagridview shows the selection
        private void cbxAMChooseExercise_SelectedIndexChanged(object sender, EventArgs e)
        {
            display_table();
        }

        //when one cell of the dgvAMmeasurements is clicked, it will collect the whole rows index
        private void dgvAMmeasurements_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //gets the exercise_id of the currently selected record from the dataGridView
            rowIndex = int.Parse(dgvAMmeasurements.Rows[e.RowIndex].Cells["id"].Value.ToString());
        }
        #endregion

        #region SENDING THE DATA TO THE DATABASE
        private void btnExport_Click(object sender, EventArgs e)
        {
            //resetting the selection in the Datagrid View (Database Table)
            rowIndex = -1;
            dgvAMmeasurements.ClearSelection();

            //insert into the exercise_data table
            int exercise_data_LastId = 0;
            Data myData = serialportsave.myData;
            try
            {
                conn.Open();

                sql = @"SELECT * FROM insert_exercise_data(
                                                            :_torque,
                                                            :_angle,
                                                            :_velocity,
                                                            :_muscle,
                                                            :_exercise,
                                                            :_repetition)";

                cmd = new NpgsqlCommand(sql, conn);

                torqueStringToDB = string.Join(";", myData.aTorqueList.ToArray());
                angleStringToDB = string.Join(";", myData.aAngleList.ToArray());
                velocityStringToDB = string.Join(";", myData.aVelocityList.ToArray());

                cmd.Parameters.AddWithValue("_torque", torqueStringToDB);
                cmd.Parameters.AddWithValue("_angle", angleStringToDB);
                cmd.Parameters.AddWithValue("_velocity", velocityStringToDB);
                cmd.Parameters.AddWithValue("_muscle", cbxEMuscle.Text);
                cmd.Parameters.AddWithValue("_exercise", cbxEExercise.Text);
                cmd.Parameters.AddWithValue("_repetition", cbxERepetitions.Text);


                exercise_data_LastId = (int)cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to export data to DB. Please record an exercise.","ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }


            if (myData != null)
            {
                //insert into the settings table
                int settings_LastId = 0;
                try
                {
                    conn.Open();

                    sql = @"SELECT * FROM insert_settings(
                                                        :_powerhead_orientation,
                                                        :_powerhead_height,
                                                        :_powerhead_position,
                                                        :_powerhead_attachment,
                                                        :_powerhead_tilt,

                                                        :_seat_height,
                                                        :_seat_orientation,
                                                        :_seat_tilt,
                                                        :_seat_position,

                                                        :_hip_flexion,
                                                        :_footplate_tilt,
                                                        :_knee_flexion,
                                                        :_ankle_flexion,
                                                        :_shoulder_abduction,
                                                        :_shoulder_flexion,
                                                        :_elbow_flexion,

                                                        :_controller_mode,
                                                        :_controller_cushion,
                                                        :_controller_sensitivity,
                                                        :_controller_rom_upper_limit,
                                                        :_controller_rom_lower_limit,
                                                        :_controller_rom_percentage,
                                                        :_controller_eccentric_speed,
                                                        :_controller_passive_speed,
                                                        :_controller_torque_limits,
                                                        :_controller_pause,
                                                        :_controller_isokinetic_speed)";

                    cmd = new NpgsqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("_powerhead_orientation", cbxPOrientation.Text);
                    cmd.Parameters.AddWithValue("_powerhead_height", nudPHeight.Text);
                    cmd.Parameters.AddWithValue("_powerhead_position", nudPPosition.Text);
                    cmd.Parameters.AddWithValue("_powerhead_attachment", cbxPAttachments.Text);
                    cmd.Parameters.AddWithValue("_powerhead_tilt", nudPTilt.Text);

                    cmd.Parameters.AddWithValue("_seat_height", nudCHeight.Text);
                    cmd.Parameters.AddWithValue("_seat_orientation", cbxCOrientation.Text);
                    cmd.Parameters.AddWithValue("_seat_tilt", nudCTilt.Text);
                    cmd.Parameters.AddWithValue("_seat_position", nudCPosition.Text);

                    cmd.Parameters.AddWithValue("_hip_flexion", nudSHipFlexion.Text);
                    cmd.Parameters.AddWithValue("_footplate_tilt", nudSFootPlateTilt.Text);
                    cmd.Parameters.AddWithValue("_knee_flexion", nudSKneeFlexion.Text);
                    cmd.Parameters.AddWithValue("_ankle_flexion", nudSAnkleFlexion.Text);
                    cmd.Parameters.AddWithValue("_shoulder_abduction", nudSShoulderAbduction.Text);
                    cmd.Parameters.AddWithValue("_shoulder_flexion", nudSShoulderFlexion.Text);
                    cmd.Parameters.AddWithValue("_elbow_flexion", nudSElbowFlexion.Text);

                    cmd.Parameters.AddWithValue("_controller_mode", cbxCoMode.Text);
                    cmd.Parameters.AddWithValue("_controller_cushion", cbxCoCushion.Text);
                    cmd.Parameters.AddWithValue("_controller_sensitivity", cbxCoSensitivity.Text);
                    cmd.Parameters.AddWithValue("_controller_rom_upper_limit", nudCoROMUpper.Text);
                    cmd.Parameters.AddWithValue("_controller_rom_lower_limit", nudCoROMLower.Text);
                    cmd.Parameters.AddWithValue("_controller_rom_percentage", nudCoPercentROM.Text);
                    cmd.Parameters.AddWithValue("_controller_eccentric_speed", nudCoEccentricSpeed.Text);
                    cmd.Parameters.AddWithValue("_controller_passive_speed", nudCoPassiveSpeed.Text);
                    cmd.Parameters.AddWithValue("_controller_torque_limits", nudCoTorqueLimit.Text);
                    cmd.Parameters.AddWithValue("_controller_pause", nudCoPause.Text);
                    cmd.Parameters.AddWithValue("_controller_isokinetic_speed", nudCoIsokineticSpeed.Text);


                    settings_LastId = (int)cmd.ExecuteScalar();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Inserted Fail. Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }



                //insert into the personal_data table
                int personal_data_LastId = 0;
                try
                {
                    conn.Open();

                    sql = @"SELECT * FROM insert_personal_data(
				                                        :_sv_number,
				                                        :_family_status,
				                                        :_email,
				                                        :_address,
				                                        :_legal_guardian,
				                                        :_religion,
				                                        :_language,
				                                        :_insurance,
				                                        :_telephone_number,
				                                        :_birth_place,
				                                        :_birth_date,
				                                        :_gender,
				                                        :_name_title)";

                    cmd = new NpgsqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("_sv_number", txtbPDSVNumber.Text);
                    cmd.Parameters.AddWithValue("_family_status", txtbPDFamilyStatus.Text);
                    cmd.Parameters.AddWithValue("_email", txtbPDEmail.Text);
                    cmd.Parameters.AddWithValue("_address", txtbPDAdress.Text);
                    cmd.Parameters.AddWithValue("_legal_guardian", txtbPDLegalGuardian.Text);
                    cmd.Parameters.AddWithValue("_religion", txtbPDReligion.Text);
                    cmd.Parameters.AddWithValue("_language", txtbPDLanguage.Text);
                    cmd.Parameters.AddWithValue("_insurance", txtbPDInsurance.Text);
                    cmd.Parameters.AddWithValue("_telephone_number", txtbPDPhoneNumber.Text);
                    cmd.Parameters.AddWithValue("_birth_place", txtbPDPlaceOfBirth.Text);
                    cmd.Parameters.AddWithValue("_birth_date", txtbPDDateOfBirth.Text);
                    cmd.Parameters.AddWithValue("_gender", txtbPDGender.Text);
                    cmd.Parameters.AddWithValue("_name_title", txtbPDTitleName.Text);


                    personal_data_LastId = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Inserted Fail. Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }



                //insert into the medical_data table
                int medical_data_LastId = 0;
                try
                {
                    conn.Open();

                    sql = @"SELECT * FROM insert_medical_data(
                                                            	:_hospital_start_date,
                                                                :_hospital_end_date,
                                                                :_hospital_address,
                                                                :_hospital_department,
                                                                :_hospital_admission_number,
                                                                :_hospital_name,
                                                                :_hospital_contact,
                                                                :_hospital_responsible_doctor,

                                                                :_diagnosis_state_at_release,
                                                                :_diagnosis_summary,
                                                                :_diagnosis_future_medication,
                                                                :_diagnosis_rehabilitation_aim,
                                                                :_diagnosis_recommended_measurements,
                                                                :_diagnosis_physical_issue,

                                                                :_medical_actions_by_hospital,
                                                                :_medical_medication_during_stay,
                                                                :_medical_medication_at_arrival,
                                                                :_medical_risk_allergies,
                                                                :_medical_previous_diseases,
                                                                :_medical_anamnesis,
                                                                :_medical_admission_reason)";

                    cmd = new NpgsqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("_hospital_start_date", txtbHIStartDate.Text);
                    cmd.Parameters.AddWithValue("_hospital_end_date", txtbHIEndDate.Text);
                    cmd.Parameters.AddWithValue("_hospital_address", txtbHIHospitalAdress.Text);
                    cmd.Parameters.AddWithValue("_hospital_department", txtbHIDepartment.Text);
                    cmd.Parameters.AddWithValue("_hospital_admission_number", txtbHIAdmissionNumber.Text);
                    cmd.Parameters.AddWithValue("_hospital_name", txtbHIHospitalName.Text);
                    cmd.Parameters.AddWithValue("_hospital_contact", txtbHIHospitalConatct.Text);
                    cmd.Parameters.AddWithValue("_hospital_responsible_doctor", txtbHIResponsibleDoctor.Text);

                    cmd.Parameters.AddWithValue("_diagnosis_state_at_release", txtbDStateRelease.Text);
                    cmd.Parameters.AddWithValue("_diagnosis_summary", txtbDSummary.Text);
                    cmd.Parameters.AddWithValue("_diagnosis_future_medication", txtbDFutureMedication.Text);
                    cmd.Parameters.AddWithValue("_diagnosis_rehabilitation_aim", txtbDRehabilitationAim.Text);
                    cmd.Parameters.AddWithValue("_diagnosis_recommended_measurements", txtbDRecommendedMeasuremnts.Text);
                    cmd.Parameters.AddWithValue("_diagnosis_physical_issue", txtbDPhysicalIssue.Text);

                    cmd.Parameters.AddWithValue("_medical_actions_by_hospital", txtbMDActionsHospital.Text);
                    cmd.Parameters.AddWithValue("_medical_medication_during_stay", txtbMDMedicationStay.Text);
                    cmd.Parameters.AddWithValue("_medical_medication_at_arrival", txtbMDMedicationArrival.Text);
                    cmd.Parameters.AddWithValue("_medical_risk_allergies", txtbMDRisksAllergies.Text);
                    cmd.Parameters.AddWithValue("_medical_previous_diseases", txtbMDPreviousDisease.Text);
                    cmd.Parameters.AddWithValue("_medical_anamnesis", txtbMDAnamnesis.Text);
                    cmd.Parameters.AddWithValue("_medical_admission_reason", txtbMDAdmissionReason.Text);


                    medical_data_LastId = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Inserted Fail. Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }


                //insert into the biodex_report table
                int biodex_report_LastId = 0;
                try
                {
                    conn.Open();

                    sql = @"SELECT * FROM insert_biodex_report()";

                    cmd = new NpgsqlCommand(sql, conn);

                    biodex_report_LastId = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Inserted Fail. Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }



                //insert into the elga_report table
                int elga_report_LastId = 0;
                try
                {
                    conn.Open();

                    sql = @"SELECT * FROM insert_elga_report()";

                    cmd = new NpgsqlCommand(sql, conn);

                    elga_report_LastId = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Inserted Fail. Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }



                //insert into the report_result table
                int report_result_LastId = 0;
                try
                {
                    conn.Open();

                    sql = @"SELECT * FROM insert_report_result()";

                    cmd = new NpgsqlCommand(sql, conn);

                    report_result_LastId = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Inserted Fail. Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }



                //insert into the uses table
                int uses_table_feedback;
                try
                {
                    conn.Open();

                    sql = @"SELECT * FROM insert_uses(:_biodex_report_id, :_exercise_id, :_settings_id)";

                    cmd = new NpgsqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("_biodex_report_id", biodex_report_LastId);
                    cmd.Parameters.AddWithValue("_exercise_id", exercise_data_LastId);
                    cmd.Parameters.AddWithValue("_settings_id", settings_LastId);

                    uses_table_feedback = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Inserted Fail. Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }



                //insert into the gives_information table
                int gives_information_feedback;
                try
                {
                    conn.Open();

                    sql = @"SELECT * FROM insert_gives_information(:_elga_report_id, :_personal_data_id, :_medical_data_id)";

                    cmd = new NpgsqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("_elga_report_id", elga_report_LastId);
                    cmd.Parameters.AddWithValue("_personal_data_id", personal_data_LastId);
                    cmd.Parameters.AddWithValue("_medical_data_id", medical_data_LastId);

                    gives_information_feedback = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Inserted Fail. Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }



                //insert into the may_contain table
                int contains_feedback;
                try
                {
                    conn.Open();

                    sql = @"SELECT * FROM insert_contains(:_report_id, :_biodex_report_id, :_elga_report_id)";

                    cmd = new NpgsqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("_report_id", report_result_LastId);
                    cmd.Parameters.AddWithValue("_biodex_report_id", biodex_report_LastId);
                    cmd.Parameters.AddWithValue("_elga_report_id", elga_report_LastId);

                    contains_feedback = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Inserted Fail. Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                MessageBox.Show("Inserted New Record Successfully Into The Database", "Record Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                /*
                #region checking if the IDs are the same for each created table to ensure, that for every measurement the Database always creates the diffrent Tables with the same ID
                List<int> lastIdListA = new List<int>() { exercise_data_LastId, settings_LastId, personal_data_LastId, medical_data_LastId };
                List<int> lastIdListB = new List<int>() { biodex_report_LastId, elga_report_LastId, report_result_LastId, exercise_data_LastId };

                if (lastIdListA.SequenceEqual(lastIdListB))     //if both lists have the same IDs, then everything is fine and the data will be stored as planned
                {
                    MessageBox.Show("Inserted New Record Successfully Into The Database", "Record Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else //if the IDs are not the same, the maximum value will be choosen. all other tables will start from the new ID
                {
                    //TO-DO Postgres Reset ID
                    #region ignore this
                    ////is needed to allign the IDs again
                    //List<int> lastIdListCal = new List<int>() { exercise_data_LastId, settings_LastId, personal_data_LastId, medical_data_LastId, biodex_report_LastId, elga_report_LastId, report_result_LastId };
                    //int maxID = lastIdListCal.Max();
                    //int renew_feedback = 0;

                    //try
                    //{
                    //	conn.Open();

                    //	sql = @"SELECT * FROM renew_ids(:_newid)";

                    //	cmd = new NpgsqlCommand(sql, conn);

                    //	cmd.Parameters.AddWithValue("_newid", maxID);

                    //	renew_feedback = (int)cmd.ExecuteScalar();

                    //	conn.Close();
                    //}
                    //catch (Exception ex)
                    //{
                    //	conn.Close();
                    //	MessageBox.Show("Renewing IDs Failed. Error: " + ex.Message);
                    //}
                    #endregion
                    MessageBox.Show("There Were Some COMPLICATIONS, WHEN EXPORTING THE DATA TO THE DATABASE. THE IDs HAVE TO BE ADJUSTED", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                #endregion
                */

                display_table();
            }
        }
        #endregion

        #region Loading Data From The DATABASE 
        //after selecting a existing record, the data can be loaded via the LOAD BUTTON: this will plot the data in the graphs window. Optionally, a CSV File can be created via the CREATE CSV FILE BUTTON
        private void btnLoad_Click(object sender, EventArgs e)
        {
            //if nothing is selected, then the user will be reminded to select a record from the table
            if (rowIndex <= 0)
            {
                MessageBox.Show("Before Loading: Please Select An Available Record From The Table 'Available Measurements'", "Please Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            //get torque from DB
            try
            {
                conn.Open();

                sql = @"SELECT * FROM return_torque(:_id)";

                cmd = new NpgsqlCommand(sql, conn);

                cmd.Parameters.AddWithValue(":_id", rowIndex);

                torqueStringFromDB = (string)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Failed. Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }



            //get angle from DB
            try
            {
                conn.Open();

                sql = @"SELECT * FROM return_angle(:_id)";

                cmd = new NpgsqlCommand(sql, conn);

                cmd.Parameters.AddWithValue(":_id", rowIndex);

                angleStringFromDB = (string)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Failed. Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }



            //get velocity from DB
            try
            {
                conn.Open();

                sql = @"SELECT * FROM return_velocity(:_id)";

                cmd = new NpgsqlCommand(sql, conn);

                cmd.Parameters.AddWithValue(":_id", rowIndex);

                velocityStringFromDB = (string)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Failed. Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            //handing the strings to the DATA object - to plot the selection
            new Data(torqueStringFromDB, velocityStringFromDB, angleStringFromDB);
        }
        #endregion

        #region Creating a CSV-file in the selected path

        //creating the CSV-File with the currently measured values and from the DB imported values
        public void createCSV(string torque, string velocity, string angle)
        {
            //spitting the string into three seperate Arrays
            string[] seperator = { ";" };
            string[] torqueArray = torque.Split(seperator, StringSplitOptions.None);
            string[] velocityArray = velocity.Split(seperator, StringSplitOptions.None);
            string[] angleArray = angle.Split(seperator, StringSplitOptions.None);

            int[] lengths = { torqueArray.Length, velocityArray.Length, angleArray.Length };
            int maxLength = lengths.Max();

            #region filling strings to put them into the CSV (exercise data and relevant settings)
            //filling strings with identification information for the header
            string timestampHeader = null;
            string exerciseHeader = null;
            string muscleHeader = null;
            string repetitionHeader = null;
            string nameTitleHeader = null;

            //filling the strings of the exercise data
            string PorientationHeader = null;
            string PtiltHeader = null;
            string PheightHeader = null;
            string PpositionHeader = null;
            string PattachementHeader = null;

            string CheightHeader = null;
            string CorientationHeader = null;
            string CtilitHeader = null;
            string CpositionHeader = null;

            string SmodeHeader = null;
            string ScushionHeader = null;
            string SsensitivityHeader = null;
            string SpauseHeader = null;
            string SespeedHeader = null;
            string SispeedHeader = null;
            string SpspeedHeader = null;
            string StlimitHeader = null;
            string SrompercentHeader = null;
            string SromlowerlimitHeader = null;
            string SromupperlimitHeader = null;

            string hipflexionHeader = null;
            string footplateHeader = null;
            string aflexionHeader = null;
            string kflexionHeader = null;
            string sabductionHeader = null;
            string sflexionHeader = null;
            string eflexionHeader = null;

            DateTime localDate = DateTime.Now;
            localDate = localDate.AddSeconds(-localDate.Second);

            //filling the strings with the values from the GUI OR DATABASE
            if (rowIndex <= 0)          //nothing is selected
            {
                timestampHeader = localDate.ToString();
                exerciseHeader = cbxEExercise.Text;
                muscleHeader = cbxEMuscle.Text;
                repetitionHeader = cbxERepetitions.Text;
                nameTitleHeader = txtbPDTitleName.Text;

                PorientationHeader = cbxPOrientation.Text;
                PtiltHeader = nudPTilt.Value.ToString();
                PheightHeader = nudPHeight.Value.ToString();
                PpositionHeader = nudPPosition.Value.ToString();
                PattachementHeader = cbxPAttachments.Text;

                CheightHeader = nudCHeight.Value.ToString();
                CorientationHeader = cbxCOrientation.Text;
                CtilitHeader = nudCTilt.Value.ToString();
                CpositionHeader = nudCPosition.Value.ToString();

                SmodeHeader = cbxCoMode.Text;
                ScushionHeader = cbxCoCushion.Text;
                SsensitivityHeader = cbxCoSensitivity.Text;
                SpauseHeader = nudCoPause.Value.ToString();
                SespeedHeader = nudCoEccentricSpeed.Value.ToString();
                SispeedHeader = nudCoIsokineticSpeed.Value.ToString();
                SpspeedHeader = nudCoPassiveSpeed.Value.ToString();
                StlimitHeader = nudCoTorqueLimit.Value.ToString();
                SrompercentHeader = nudCoPercentROM.Value.ToString();
                SromlowerlimitHeader = nudCoROMLower.Value.ToString();
                SromupperlimitHeader = nudCoROMUpper.Value.ToString();

                hipflexionHeader = nudSHipFlexion.Value.ToString();
                footplateHeader = nudSFootPlateTilt.Value.ToString();
                aflexionHeader = nudSAnkleFlexion.Value.ToString();
                kflexionHeader = nudSKneeFlexion.Value.ToString();
                sabductionHeader = nudSShoulderAbduction.Value.ToString();
                sflexionHeader = nudSShoulderFlexion.Value.ToString();
                eflexionHeader = nudSElbowFlexion.Value.ToString();
            }
            else
            {
                conn.Open();

                timestampHeader = dgvAMmeasurements.CurrentRow.Cells["created_at"].Value.ToString();
                exerciseHeader = dgvAMmeasurements.CurrentRow.Cells["exercise"].Value.ToString();
                muscleHeader = dgvAMmeasurements.CurrentRow.Cells["muscle"].Value.ToString();
                repetitionHeader = dgvAMmeasurements.CurrentRow.Cells["repetition"].Value.ToString();
                nameTitleHeader = dgvAMmeasurements.CurrentRow.Cells["name_title"].Value.ToString();


                //POWERHEAD
                sql = @"SELECT * FROM get_porientation(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                PorientationHeader = (string)cmd.ExecuteScalar();

                sql = @"SELECT * FROM get_powerhead_tilt(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                PtiltHeader = (string)cmd.ExecuteScalar();

                sql = @"SELECT * FROM get_powerhead_height(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                PheightHeader = (string)cmd.ExecuteScalar();

                sql = @"SELECT * FROM get_powerhead_position(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                PpositionHeader = (string)cmd.ExecuteScalar();

                sql = @"SELECT * FROM get_powerhead_attachment(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                PattachementHeader = (string)cmd.ExecuteScalar();



                //SEAT/CHAIR
                sql = @"SELECT * FROM get_seat_height(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                CheightHeader = (string)cmd.ExecuteScalar();

                sql = @"SELECT * FROM get_seat_orientation(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                CorientationHeader = (string)cmd.ExecuteScalar();

                sql = @"SELECT * FROM get_seat_tilt(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                CtilitHeader = (string)cmd.ExecuteScalar();

                sql = @"SELECT * FROM get_seat_position(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                CpositionHeader = (string)cmd.ExecuteScalar();



                //CONTROLLER
                sql = @"SELECT * FROM get_controller_mode(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                SmodeHeader = (string)cmd.ExecuteScalar();

                sql = @"SELECT * FROM get_controller_cushion(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                ScushionHeader = (string)cmd.ExecuteScalar();

                sql = @"SELECT * FROM get_controller_sensitivity(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                SsensitivityHeader = (string)cmd.ExecuteScalar();

                sql = @"SELECT * FROM get_controller_pause(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                SpauseHeader = (string)cmd.ExecuteScalar();

                sql = @"SELECT * FROM get_controller_eccentric_speed(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                SespeedHeader = (string)cmd.ExecuteScalar();

                sql = @"SELECT * FROM get_controller_isokinetic_speed(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                SispeedHeader = (string)cmd.ExecuteScalar();

                sql = @"SELECT * FROM get_controller_passive_speed(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                SpspeedHeader = (string)cmd.ExecuteScalar();

                sql = @"SELECT * FROM get_controller_torque_limits(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                StlimitHeader = (string)cmd.ExecuteScalar();

                sql = @"SELECT * FROM get_controller_rom_percentage(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                SrompercentHeader = (string)cmd.ExecuteScalar();

                sql = @"SELECT * FROM get_controller_rom_lower_limit(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                SromlowerlimitHeader = (string)cmd.ExecuteScalar();

                sql = @"SELECT * FROM get_controller_rom_upper_limit(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                SromupperlimitHeader = (string)cmd.ExecuteScalar();




                //SET-UP AND POSITIONING
                sql = @"SELECT * FROM get_hip_flexion(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                hipflexionHeader = (string)cmd.ExecuteScalar();

                sql = @"SELECT * FROM get_footplate_tilt(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                footplateHeader = (string)cmd.ExecuteScalar();

                sql = @"SELECT * FROM get_ankle_flexion(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                aflexionHeader = (string)cmd.ExecuteScalar();

                sql = @"SELECT * FROM get_knee_flexion(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                kflexionHeader = (string)cmd.ExecuteScalar();

                sql = @"SELECT * FROM get_shoulder_abduction(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                sabductionHeader = (string)cmd.ExecuteScalar();

                sql = @"SELECT * FROM get_shoulder_flexion(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                sflexionHeader = (string)cmd.ExecuteScalar();

                sql = @"SELECT * FROM get_elbow_flexion(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(":_id", rowIndex);
                eflexionHeader = (string)cmd.ExecuteScalar();

                conn.Close();
            }

            //creating the CSV-File
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.AppendLine("Timestamp;" + timestampHeader);
            sb.AppendLine("Exercise;" + exerciseHeader);
            sb.AppendLine("Muscle;" + muscleHeader);
            sb.AppendLine("Repetition;" + repetitionHeader);
            sb.AppendLine("Name and Title;" + nameTitleHeader).AppendLine();

            sb.AppendLine("POWERHEAD SETTINGS:");
            sb.AppendLine("Orientation [°];" + PorientationHeader);
            sb.AppendLine("Tilt [°];" + PtiltHeader);
            sb.AppendLine("Height [cm];" + PheightHeader);
            sb.AppendLine("Position [cm];" + PpositionHeader);
            sb.AppendLine("Attachment;" + PattachementHeader).AppendLine();

            sb.AppendLine("CHAIR ADJUSTMENTS:");
            sb.AppendLine("Height [cm];" + CheightHeader);
            sb.AppendLine("Orientation [°];" + CorientationHeader);
            sb.AppendLine("Tilt [°];" + CtilitHeader);
            sb.AppendLine("Position [cm];" + CpositionHeader).AppendLine();

            sb.AppendLine("CONTROLLER SETTINGS:");
            sb.AppendLine("Mode;" + SmodeHeader);
            sb.AppendLine("Cushion;" + ScushionHeader);
            sb.AppendLine("Sensitivity;" + SsensitivityHeader);
            sb.AppendLine("Pause [s];" + SpauseHeader);
            sb.AppendLine("Eccentric Speed [°/s];" + SespeedHeader);
            sb.AppendLine("Isokinetic Speed [°/s];" + SispeedHeader);
            sb.AppendLine("Passive Speed [°/s];" + SpspeedHeader);
            sb.AppendLine("Torque Limit[ft·lbf];" + StlimitHeader);
            sb.AppendLine("ROM Percentage [%];" + SrompercentHeader);
            sb.AppendLine("ROM Lower Limit;" + SromlowerlimitHeader);
            sb.AppendLine("ROM Upper Limit;" + SromupperlimitHeader).AppendLine();

            sb.AppendLine("SET-UP AND POSITIONING:");
            sb.AppendLine("Hip Flexion [°];" + hipflexionHeader);
            sb.AppendLine("Foot Plate Tilt [°];" + footplateHeader);
            sb.AppendLine("Ankle Flexion [°];" + aflexionHeader);
            sb.AppendLine("Knee Flexion[°];" + kflexionHeader);
            sb.AppendLine("Shoulder Abduction[°];" + sabductionHeader);
            sb.AppendLine("Shoulder Flexion[°];" + sflexionHeader);
            sb.AppendLine("Elbow Flexion[°];" + eflexionHeader).AppendLine().AppendLine().AppendLine();

            #endregion

            //filling the CSV File with the exercise values TORQUE, VELOCITY and ANGLE
            sb.AppendLine("Torque;Velocity;Angle");
            for (int i = 0; i < maxLength; i++)
            {
                sb.AppendLine(torqueArray[i] + ";" + velocityArray[i] + ";" + angleArray[i]);           //starts new line with the three values and in between there is a ; (torque; velocity; angle)
            }

            //pop up window, to save the data ... tips from: https://www.youtube.com/watch?v=5hQQg7S_5GQ
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter write = new StreamWriter(File.Create(save.FileName), new UTF8Encoding(true));
                write.Write(sb.ToString());
                write.Dispose();
            }
        }


        private void btnCreateCSV_Click(object sender, EventArgs e)
        {
            if (rowIndex <= 0)      // if-statement means nothing is selected from the Data Table (Available Measurement)
            {
                Data myData = serialportsave.myData;
                try
                {
                    torqueStringToDB = string.Join(";", myData.aTorqueList.ToArray());
                    angleStringToDB = string.Join(";", myData.aAngleList.ToArray());
                    velocityStringToDB = string.Join(";", myData.aVelocityList.ToArray());
                    createCSV(torqueStringToDB, velocityStringToDB, angleStringToDB);
                }
                catch (Exception)
                {
                    MessageBox.Show("Please select a record from the table or record a new exercise", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    createCSV(torqueStringFromDB, velocityStringFromDB, angleStringFromDB);
                }
                catch (Exception)
                {
                    MessageBox.Show("You have to 'Load And Plot' the record first", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

    }
}

/*
 * ¯\_(ツ)_/¯
 *Just some attempts of implementing plotting with vs chart
 *Left here in the case it could be needed 
 *Everything below is commented out
 * ¯\_(ツ)_/¯
 */
#region savebutton_click, loadtestbutton_click
/*
//fills _mProperties and _patientData with values from GUI
private async void btnSave_ClickAsync(object sender, EventArgs e)
{
    #region some useful test code
    /*_mProperties = new MProperties(cbxEExercise.GetItemText(cbxEExercise.SelectedItem),
                                              cbxEMuscle.GetItemText(cbxEMuscle.SelectedItem),
                                              cbxERepetitions.GetItemText(cbxERepetitions.SelectedItem),

                                              cbxPOrientation.GetItemText(cbxPOrientation.SelectedItem),
                                              nudPTilt.Value.ToString(),
                                              nudPHeight.Value.ToString(),
                                              nudPPosition.Value.ToString(),
                                              cbxPAttachments.GetItemText(cbxPAttachments.SelectedItem),

                                              cbxCOrientation.GetItemText(cbxCOrientation.SelectedItem),
                                              nudCTilt.Value.ToString(),
                                              nudCHeight.Value.ToString(),
                                              nudCPosition.Value.ToString(),

                                              cbxCoMode.GetItemText(cbxCoMode.SelectedItem),
                                              cbxCoCushion.GetItemText(cbxCoCushion.SelectedItem),
                                              cbxCoSensitivity.GetItemText(cbxCoSensitivity.SelectedItem),
                                              nudCoPause.Value.ToString(),
                                              nudCoEccentricSpeed.Value.ToString(),
                                              nudCoPassiveSpeed.Value.ToString(),
                                              nudCoIsokineticSpeed.Value.ToString(),
                                              nudCoTorqueLimit.Value.ToString(),
                                              nudCoPercentROM.Value.ToString(),
                                              nudCoROMLower.Value.ToString(),
                                              nudCoROMUpper.Value.ToString(),

                                              nudSHipFlexion.Value.ToString(),
                                              nudSFootPlateTilt.Value.ToString(),
                                              nudSAnkleFlexion.Value.ToString(),
                                              nudSKneeFlexion.Value.ToString(),
                                              nudSShoulderAbduction.Value.ToString(),
                                              nudSShoulderFlexion.Value.ToString(),
                                              nudSElbowFlexion.Value.ToString());

    _patientData = new PatientData(txtbPDTitleName.Text,
                                              txtbPDSVNumber.Text,
                                              txtbPDPlaceOfBirth.Text,
                                              txtbPDGender.Text,
                                              txtbPDDateOfBirth.Text,
                                              txtbPDPhoneNumber.Text,
                                              txtbPDInsurance.Text,
                                              txtbPDLanguage.Text,
                                              txtbPDReligion.Text,
                                              txtbPDLegalGuardian.Text,
                                              txtbPDAdress.Text,
                                              txtbPDEmail.Text,
                                              txtbPDFamilyStatus.Text,

                                              txtbHIHospitalName.Text,
                                              txtbHIDepartment.Text,
                                              txtbHIHospitalAdress.Text,
                                              txtbHIHospitalConatct.Text,
                                              txtbHIStartDate.Text,
                                              txtbHIEndDate.Text,
                                              txtbHIAdmissionNumber.Text,
                                              txtbHIResponsibleDoctor.Text,

                                              txtbMDAdmissionReason.Text,
                                              txtbMDAnamnesis.Text,
                                              txtbMDPreviousDisease.Text,
                                              txtbMDRisksAllergies.Text,
                                              txtbMDMedicationArrival.Text,
                                              txtbMDMedicationStay.Text,
                                              txtbMDActionsHospital.Text,

                                              txtbDStateRelease.Text,
                                              txtbDPhysicalIssue.Text,
                                              txtbDRecommendedMeasuremnts.Text,
                                              txtbDRehabilitationAim.Text,
                                              txtbDFutureMedication.Text,
                                              txtbDSummary.Text);

    //DataAccessObject DAO = new DataAccessObject();
    //float[] torque = convertDoubleToFloat(_data.Torque);
    //float[] angle = convertDoubleToFloat(_data.Angle);
    //float[] angleVelocity = convertDoubleToFloat(_data.Velocity);
    //Settings settings = new Settings(-1, cbxPOrientation.GetItemText(cbxPOrientation.SelectedItem), nudPHeight.Value.ToString(), nudPPosition.Value.ToString(), cbxPAttachments.GetItemText(cbxPAttachments.SelectedItem), nudPTilt.Value.ToString(), nudCHeight.Value.ToString(), cbxCOrientation.GetItemText(cbxCOrientation.SelectedItem), nudCTilt.Value.ToString(), nudCPosition.Value.ToString(), nudSHipFlexion.Value.ToString(), nudSFootPlateTilt.Value.ToString(), nudSKneeFlexion.Value.ToString(), nudSAnkleFlexion.Value.ToString(), nudSShoulderAbduction.Value.ToString(), nudSShoulderFlexion.Value.ToString(), cbxCoMode.GetItemText(cbxCoMode.SelectedItem), cbxCoCushion.GetItemText(cbxCoCushion.SelectedItem), cbxCoSensitivity.GetItemText(cbxCoSensitivity.SelectedItem), nudCoROMUpper.Value.ToString(), nudCoROMLower.Value.ToString(), nudCoPercentROM.Value.ToString(), nudCoEccentricSpeed.Value.ToString(), nudCoPassiveSpeed.Value.ToString(), nudCoTorqueLimit.Value.ToString(), nudCoPause.Value.ToString(),nudCoIsokineticSpeed.Value.ToString(), nudSElbowFlexion.Value.ToString());
    //long settingsID = DAO.insertIntoSettings(settings);
    //BiodexReport biodexReport = new BiodexReport(-1, torque,angle, angleVelocity,cbxEExercise.GetItemText(cbxEExercise.SelectedItem), cbxEMuscle.GetItemText(cbxEMuscle.SelectedItem), cbxERepetitions.GetItemText(cbxERepetitions.SelectedItem), settingsID);
    //await DAO.insertIntoBiodexReportAsync(biodexReport);
    //medicalData medicaldata = new medicalData(-1, txtbHIStartDate.Text, txtbHIEndDate.Text, txtbHIHospitalAdress.Text, txtbHIDepartment.Text, txtbHIAdmissionNumber.Text, txtbHIHospitalName.Text, txtbHIHospitalConatct.Text, txtbHIResponsibleDoctor.Text, txtbDStateRelease.Text, txtbDSummary.Text, txtbDFutureMedication.Text, txtbDRehabilitationAim.Text, txtbDRecommendedMeasuremnts.Text, txtbDPhysicalIssue.Text, txtbMDActionsHospital.Text, txtbMDMedicationStay.Text, txtbMDMedicationArrival.Text, txtbMDRisksAllergies.Text, txtbMDPreviousDisease.Text, txtbMDAnamnesis.Text, txtbMDAdmissionReason.Text);
    //medicaldata.medicalDataID =  DAO.insertIntoMedicalDataAsync(medicaldata);
    //personalData personaldata = new personalData(txtbPDSVNumber.Text, txtbPDFamilyStatus.Text, txtbPDEmail.Text, txtbPDAdress.Text, txtbPDLegalGuardian.Text, txtbPDReligion.Text, txtbPDLanguage.Text, txtbPDInsurance.Text, txtbPDPhoneNumber.Text, txtbPDPlaceOfBirth.Text, txtbPDDateOfBirth.Text, txtbPDGender.Text, txtbPDTitleName.Text);
    //personaldata.SV_Number =  DAO.insertIntoPersonalDataAsync(personaldata);
    //elgaReport elgareport = new elgaReport(-1);
    //elgareport.elgaID = DAO.insertIntoElgaReport(elgareport);
    //Proband proband = new Proband()
    #endregion some useful test code 
    //REFRESH BUTTON, displays the updated values from the Database
    display_table();
    MessageBox.Show("Table Displays Current Database Entries", "Table Refreshed", MessageBoxButtons.OK, MessageBoxIcon.Information);
}

/*
 * just for test purposes
 * reads test file for plotting and starts new thread for plotting
 * function will be removed when serialport+ loadbutton_click is implemented
 */
/*
private void btnLoadTest_Click(object sender, EventArgs e)
{
    string path = "C:/Users/jgtha/OneDrive/BBE/Biodex/Biodex Client/csv data to read for load(test)/Armin_Messung";
    int[][] data = readCSV(path);
    _data = new Data(data);
    refreshCharts();
    threadAddValuesToChart = new Thread(new ThreadStart(addValuesToChart));
    threadAddValuesToChart.Start();           
}*/

/*
* reads test csv file and returns int data array
* function will be deleted as soon as serialport and loadbutton work
*/
/*
int[][] readCSV(string path)
{
    if (File.Exists(path))
    {
        string[] lines = File.ReadAllLines(path, Encoding.Default);
        string[][] dataString = new string[lines.Length][];
        int[][] data = new int[lines.Length][];

        for (int i = 0; i < lines.Length; i++)
        {
            dataString[i] = lines[i].Split(',');
            int[] temp = new int[dataString[i].Length];
            for (int j = 0; j < dataString[i].Length; j++)
            {
                temp[j] = Convert.ToInt32(dataString[i][j]);
            }
            data[i] = temp;
        }
        data = Transpose(data);
        return data;
    }
    else
    {
        throw new FileNotFoundException();
    }
}

/*
 * transposes parameter matrix
 * function will be deleted as soon as serialport and loadbutton work
 */
/*
int[][] Transpose(int[][] matrix)
{
    int w = matrix.GetLength(0);
    int h = matrix[0].GetLength(0);

    int[][] result = new int[h][];
    int[] temp = null;

    for (int i = 0; i < h; i++)
    {
        temp = new int[w];
        for (int j = 0; j < w; j++)
        {
            temp[j] = matrix[j][i];
        }
        result[i] = temp;
    }

    return result;
}


private float[] convertDoubleToFloat(double[] doubleArray)
{
    try
    {
        float[] floatArray = new float[doubleArray.Length];
        for (int i = 0; i < doubleArray.Length; i++)
        {
            floatArray[i] = (float)doubleArray[i];
        }
        return floatArray;
    }
    catch (Exception e)
    {
        return null;
    }
}
*/

#endregion, 

#region safecalldelegate to update chart live
/*
       delegate void SafeCallDelegate(ValuePoint valuePoint);

       private void SetValuePointSafe(ValuePoint valuePoint)
       {
           if (_FormGraphs.chart1.InvokeRequired)
           {
               var d = new SafeCallDelegate(SetValuePointSafe);
               _FormGraphs.chart1.Invoke(d, new object[] { valuePoint });
               _FormGraphs.chart1.Series["AngularSpeed"].Points.AddXY(valuePoint.Frame, valuePoint.Value);
           }
           else
           {
               _FormGraphs.chart1.Series["AngularSpeed"].Points.AddXY(valuePoint.Frame, valuePoint.Value);
           }
       }*/

///Trying to update chart with its own loop
//System.Windows.Forms.DataVisualization.Charting.Chart chart = _FormGraphs.chart1;
//_FormGraphs.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
/* for (int i=0;i<graphData.Time.Length;i++)
 {

     ValuePoint valuePoint = new ValuePoint(graphData.Time[i], graphData.Angle[i]);
     //SetValuePointSafe(valuePoint);
     _FormGraphs.chart1.Series["AngularSpeed"].Points.AddXY(valuePoint.Frame,valuePoint.Value);
     Thread.Sleep(10);
 }*/
#endregion

#region backgroundworker chart live update
//using backgroundworkers in loop to update chart live does not work
//maybe works good using serialdatareceivedeenthandler which calls method that calls async work
/*
private BackgroundWorker backgroundWorker1;

  backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker1_RunWorkerCompleted);

//bool flagUpdatingChart = true;
// int i = 0;
 while (flagUpdatingChart)
 {

     if(!backgroundWorker1.IsBusy)
     {
         backgroundWorker1.RunWorkerAsync(i);
         i++;
     }
     if(i==graphData.Time.Length)
     {
         i = 0;
         flagUpdatingChart = false;
     }
 }*/
//backgroundWorker1.RunWorkerAsync(i);
/* for(int i=0;i<graphData.Time.Length;)
 {
     if (!backgroundWorker1.IsBusy)
     {
         backgroundWorker1.RunWorkerAsync(i);
         i++;
     }
 }*/

/* private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
       {
           int i = (int)e.Argument;
           ValuePoint valuePoint = new ValuePoint(graphData.Time[i], graphData.Angle[i]);

           e.Result = valuePoint;
       }*/

/*
private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
{
    ValuePoint valuePoint = (ValuePoint)e.Result;
    _FormGraphs.chart1.Series["AngularSpeed"].Points.AddXY(valuePoint.Frame, valuePoint.Value);
    backgroundWorker1.CancelAsync();
}*/


#endregion

#region plot interaction
/*
 * generates Valuepoint objects which are added to the chart series
 */

//void addValuesToChart()
//{ 
//    //for live plotting loop has to be removed and function has to be called from event handler
//    // eventargs need to contain current values from serialport
//    for (int i = 0; i < _data.Time.Length; i++)
//    {
//        //maybe change those points to the calibrated version
//        ValuePoint ValuePointTorque=new ValuePoint(_data.Time[i], _data.Torque[i]);
//        ValuePoint ValuePointVelocity = new ValuePoint(_data.Time[i], _data.Velocity[i]);
//        ValuePoint ValuePointAngle = new ValuePoint(_data.Time[i], _data.Angle[i]);
//        ChartValuesTorqueValues.Add(ValuePointTorque);
//        ChartValuesVelocityValues.Add(ValuePointVelocity);
//        ChartValuesAngleValues.Add(ValuePointAngle);
//        //Thread.Sleep(10); //for showing liveplotting
//        //Update max and min of axis for liveplotting
//    }
//    threadAddValuesToChart.Abort();
//}

/*
 * clears chartvalue members and initializes current Lineseries with new ones
 * Is needed because _FormGraphs.chartTorque.Series.Clear() does not work.
 * This is an already known issue by the livecharts comunity but it has not been solved yet
 */
//public void refreshCharts()
//{
//    ChartValuesTorqueValues.Clear();
//    ChartValuesVelocityValues.Clear();
//    ChartValuesAngleValues.Clear();

//    SeriesCollection helperTorqueSeries = new SeriesCollection
//    {
//            new LineSeries
//            {
//                Values=ChartValuesTorqueValues,
//                Title="Torque",
//                StrokeThickness = 1, //maybe change
//                PointGeometrySize = 0,
//                Stroke=System.Windows.Media.Brushes.LightSkyBlue, //lightskyblue, tomato, khaki
//                Fill=System.Windows.Media.Brushes.Transparent
//            }
//    };

//    SeriesCollection helperVelocitySeries = new SeriesCollection
//    {
//            new LineSeries
//            {
//                Values=ChartValuesVelocityValues,
//                Title="Angle Velocity",
//                StrokeThickness = 1, //maybe change
//                PointGeometrySize = 0,
//                Stroke=System.Windows.Media.Brushes.Tomato, //lightskyblue, tomato, khaki
//                Fill=System.Windows.Media.Brushes.Transparent
//            }
//    };

//    SeriesCollection helperAngleSeries = new SeriesCollection
//    {
//            new LineSeries
//            {
//                Values=ChartValuesAngleValues,
//                Title="Angle",
//                StrokeThickness = 1, //maybe change
//                PointGeometrySize = 0,
//                Stroke=System.Windows.Media.Brushes.Khaki, //lightskyblue, tomato, khaki
//                Fill=System.Windows.Media.Brushes.Transparent
//            }
//    };

//    _FormGraphs.chartTorque.Series = helperTorqueSeries;
//    _FormGraphs.chartVelocity.Series = helperVelocitySeries;
//    _FormGraphs.chartAngle.Series = helperAngleSeries;
//}

/*
 * class which generates Valuepoint objects that can be added to a Series
 * maybe this class will be pushed into its own cs file
 */
//public class ValuePoint
//{
//    public double Frame { get; }
//    public double Value { get; }

//    public ValuePoint(double frame, double value)
//    {
//        Frame = frame;
//        Value = value;
//    }
//}
#endregion
