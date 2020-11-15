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

namespace Biodex_Client
{
    public partial class formMeasurementProperties : Form
    {
        //initializing of necessary members
        formGraphs _FormGraphs = null;
        Data _data = null;
        MProperties _mProperties = null;

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
            foreach (Control ctl in getControls())
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            _mProperties = new MProperties(cbxEExercise.GetItemText(cbxEExercise.SelectedItem),
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

