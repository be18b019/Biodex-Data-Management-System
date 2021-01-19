using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Biodex_Client
{
    public partial class formGraphs : Form
    {
        Data _data = null;
        SerialPort BiodexSerialPort;
        private SerialPortSave settings;


        public formGraphs()
        {
           

            InitializeComponent();
        }

        public formGraphs(Data data, SerialPortSave settings)
        {
            _data = data;
            this.settings = settings;
            InitializeComponent();
        }

        /*
         * Serial Port opens and Handler will be started
         */
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                //Biodex_Client.FormMeasurementProperties.refreshCharts();
                Biodex_Client.GraphPlotting.refreshCharts();
                _data.ClearLists();

                BiodexSerialPort = new SerialPort(settings.sSerialPort);

                BiodexSerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

                BiodexSerialPort.Open();
            }
            catch (Exception SerialPortOpenException)
            {
                if (settings.sSerialPort == null)
                {
                    MessageBox.Show("Before plotting, a serial port has to chosen in the Microcontroller Status tab.", "NO SERIAL PORT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Serial port could not be opened.", "SERIAL PORT ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                       
                System.Console.WriteLine("Serial Port could not be opened " + SerialPortOpenException.Message);
            }

        }

        /*
         * Received Data is added to Data class and plotted on the Graph
         */
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {


            SerialPort sp = (SerialPort)sender;
            sp.NewLine = "\n"; // maybe the data from Biodex has other new line indicators
            sp.ReadTimeout = 1000;
            try
            {
                while (sp.BytesToRead > 0)
                {
                    string indata = sp.ReadLine();
                    var values = indata.Split(','); // maybe the data from Biodex has other signal indicators
                    _data.AddtoLists(Convert.ToDouble(values[0]), Convert.ToDouble(values[1]), Convert.ToDouble(values[2]));

                    var time = _data.aTimeList.Last();
                    var torque = _data.aTorqueList.Last();
                    var velocity = _data.aVelocityList.Last();
                    var angle = _data.aAngleList.Last();

                    //Biodex_Client.FormMeasurementProperties.ChartValuesTorqueValues.Add(new formMeasurementProperties.ValuePoint(time, torque));
                    Biodex_Client.GraphPlotting.m_aChartValuesTorqueValues.Add(new ValuePoint(time, torque));
                    //Biodex_Client.FormMeasurementProperties.ChartValuesVelocityValues.Add(new formMeasurementProperties.ValuePoint(time, velocity));
                    Biodex_Client.GraphPlotting.m_aChartValuesVelocityValues.Add(new ValuePoint(time, velocity));
                    //Biodex_Client.FormMeasurementProperties.ChartValuesAngleValues.Add(new formMeasurementProperties.ValuePoint(time, angle));
                    Biodex_Client.GraphPlotting.m_aChartValuesAngleValues.Add(new ValuePoint(time, angle));

                }
            }
            catch (TimeoutException timeoutException)
            {
                MessageBox.Show("Timeout during read " + timeoutException.Message,"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {

                BiodexSerialPort.Close();
                BiodexSerialPort.DataReceived -= DataReceivedHandler;

                settings.myData = _data;

            }
            catch (Exception)
            {
                MessageBox.Show("Start a recording before you try to stop it!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
        }
    }
}

