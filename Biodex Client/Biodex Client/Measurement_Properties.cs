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

namespace Biodex_Client
{
    public partial class formMeasurementProperties : Form
    {
        //initializing of necessary members
        formGraphs _FormGraphs = null;
        Data _data = null;
        MProperties _mProperties = null;
        PatientData _patientData = null;

        public ChartValues<ValuePoint> ChartValuesTorqueValues { get; set; }
        public ChartValues<ValuePoint> ChartValuesVelocityValues { get; set; }
        public ChartValues<ValuePoint> ChartValuesAngleValues { get; set; }

        Thread threadAddValuesToChart;


        #region formMeasurement Constructors, Load function, disable nud Scroll
        public formMeasurementProperties()
        {
            InitializeComponent();        
        }

        /*
         * custom constructor for initializing serveral objects
         */
        public formMeasurementProperties(formGraphs FormGraphs, Data data)
        {
            _FormGraphs = FormGraphs;
            _data = data;

            var mapper = Mappers.Xy<ValuePoint>()
                 .X(model => model.Frame)
                 .Y(model => model.Value);

            Charting.For<ValuePoint>(mapper);

            ChartValuesTorqueValues = new ChartValues<ValuePoint>();
            ChartValuesVelocityValues = new ChartValues<ValuePoint>();
            ChartValuesAngleValues = new ChartValues<ValuePoint>();


            //chartTorqueInitialisation
            _FormGraphs.chartTorque.DisableAnimations = true;
            _FormGraphs.chartTorque.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Frames",
                //DisableAnimations = true,
                //MaxValue = graphData.Time.Length
            });

            _FormGraphs.chartTorque.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Torque",
                //DisableAnimations = true,
                //MaxValue = 600, 
                //MinValue=400
            });

            

            //chartVelocityInitialisation
            _FormGraphs.chartVelocity.DisableAnimations = true;
            _FormGraphs.chartVelocity.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Frames",
                //DisableAnimations = true,
                //MaxValue = graphData.Time.Length
            });

            _FormGraphs.chartVelocity.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Angular Velocity",
                //DisableAnimations = true,
                //MaxValue = 600, 
                //MinValue=400
            });

           

            //chartAngleInitialisation
            _FormGraphs.chartAngle.DisableAnimations = true;
            _FormGraphs.chartAngle.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Frames",
                //DisableAnimations = true,
                //MaxValue = graphData.Time.Length
            });

            _FormGraphs.chartAngle.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Angle",
                //DisableAnimations = true,
                //MaxValue = 600, 
                //MinValue=400
            });

            InitializeComponent();
        }


        /*
       * added getControls function to:
       * https://stackoverflow.com/questions/59862561/how-to-disable-scrolling-on-a-numericupdown-in-a-windows-form
       * adds an Eventhandler to MousewheelEvents for disabling scrolling
       */
        private void formMeasurementProperties_Load(object sender, EventArgs e)
        {
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

        #region loadtestbutton and graphs
        /*
         * just for test purposes
         * reads test file for plotting and starts new thread for plotting
         * function will be removed when serialport+ loadbutton_click is implemented
         */
        private void btnLoadTest_Click(object sender, EventArgs e)
        {
            string path = "C:/Users/jgtha/OneDrive/BBE/Biodex/Biodex Client/csv data to read for load(test)/Armin_Messung";
            int[][] data = readCSV(path);
            _data = new Data(data);
            refreshCharts();
            threadAddValuesToChart = new Thread(new ThreadStart(addValuesToChart));
            threadAddValuesToChart.Start();           
        }

        /*
         * generates Valuepoint objects which are added to the chart series
         */
        void addValuesToChart()
        { 
            //for live plotting loop has to be removed and function has to be called from event handler
            // eventargs need to contain current values from serialport
            for (int i = 0; i < _data.Time.Length; i++)
            {
                //maybe change those points to the calibrated version
                ValuePoint ValuePointTorque=new ValuePoint(_data.Time[i], _data.Torque[i]);
                ValuePoint ValuePointVelocity = new ValuePoint(_data.Time[i], _data.Velocity[i]);
                ValuePoint ValuePointAngle = new ValuePoint(_data.Time[i], _data.Angle[i]);
                ChartValuesTorqueValues.Add(ValuePointTorque);
                ChartValuesVelocityValues.Add(ValuePointVelocity);
                ChartValuesAngleValues.Add(ValuePointAngle);
                //Thread.Sleep(10); //for showing liveplotting
                //Update max and min of axis for liveplotting
            }
            threadAddValuesToChart.Abort();
        }

        /*
         * clears chartvalue members and initializes current Lineseries with new ones
         * Is needed because _FormGraphs.chartTorque.Series.Clear() does not work.
         * This is an already known issue by the livecharts comunity but it has not been solved yet
         */
        public void refreshCharts()
        {
            ChartValuesTorqueValues.Clear();
            ChartValuesVelocityValues.Clear();
            ChartValuesAngleValues.Clear();

            SeriesCollection helperTorqueSeries = new SeriesCollection
            {
                    new LineSeries
                    {
                        Values=ChartValuesTorqueValues,
                        Title="Torque",
                        StrokeThickness = 1, //maybe change
                        PointGeometrySize = 0,
                        Stroke=System.Windows.Media.Brushes.LightSkyBlue, //lightskyblue, tomato, khaki
                        Fill=System.Windows.Media.Brushes.Transparent
                    }
            };

            SeriesCollection helperVelocitySeries = new SeriesCollection
            {
                    new LineSeries
                    {
                        Values=ChartValuesVelocityValues,
                        Title="Angle Velocity",
                        StrokeThickness = 1, //maybe change
                        PointGeometrySize = 0,
                        Stroke=System.Windows.Media.Brushes.Tomato, //lightskyblue, tomato, khaki
                        Fill=System.Windows.Media.Brushes.Transparent
                    }
            };

            SeriesCollection helperAngleSeries = new SeriesCollection
            {
                    new LineSeries
                    {
                        Values=ChartValuesAngleValues,
                        Title="Angle",
                        StrokeThickness = 1, //maybe change
                        PointGeometrySize = 0,
                        Stroke=System.Windows.Media.Brushes.Khaki, //lightskyblue, tomato, khaki
                        Fill=System.Windows.Media.Brushes.Transparent
                    }
            };

            _FormGraphs.chartTorque.Series = helperTorqueSeries;
            _FormGraphs.chartVelocity.Series = helperVelocitySeries;
            _FormGraphs.chartAngle.Series = helperAngleSeries;
        }

        /*
         * class which generates Valuepoint objects that can be added to a Series
         * maybe this class will be pushed into its own cs file
         */
        public class ValuePoint
        {
            public double Frame { get; }
            public double Value { get; }

            public ValuePoint(double frame, double value)
            {
                Frame = frame;
                Value = value;
            }
        }

        /*
         * reads test csv file and returns int data array
         * function will be deleted as soon as serialport and loadbutton work
         */
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

        #endregion


        /*
         * fills _mProperties and _patientData with values from GUI
         */
        private async void btnSave_ClickAsync(object sender, EventArgs e)
        {
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
                                                      txtbDSummary.Text);*/

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
            catch(Exception e)
            {
                return null;
            }
        }

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
                _mProperties = null;
                _patientData = null;
            }
        }

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
				DummyData = "Name Titel|$|SV-Number|$|Gender|$|Birth Date|$|Birth Place|$|Phone Number|$|Insurance|$|Language|$|Religion|$|Guardian|$|Adress|$|Email|$|Family Status|$|Hospital Name|$|Hospital Department|$|Hospital Adress|$|Hospital Contact|$|Start Date|$|End Date|$|Admission Number|$|Responsible Doctor|$|Admission Reason|$|Anamnesis|$|Previous Diseases|$|Risk and Allergies|$|Medication At Arrival|$|Medication During Stay|$|Actions By Hospital|$|State At Release|$|Pysical Issue|$|Recommended Measurements|$|Rehabilitation Aim|$|Future Medication|$|Diagnosis Summary|$||$||$|";
			}

			string[] DummyDataArray = DummyData.Split(seperator, StringSplitOptions.RemoveEmptyEntries);

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
		}
	}
}

/*
 * ¯\_(ツ)_/¯
 *Just some attempts of implementing plotting with vs chart
 *Left here in the case it could be needed 
 * ¯\_(ツ)_/¯
 */
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

