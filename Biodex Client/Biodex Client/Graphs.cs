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
                Biodex_Client.FormMeasurementProperties.refreshCharts();
                _data.ClearLists();

                BiodexSerialPort = new SerialPort(settings.sSerialPort);

                BiodexSerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

                BiodexSerialPort.Open();
            }
            catch (Exception SerialPortOpenException)
            {
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

                    Biodex_Client.FormMeasurementProperties.ChartValuesTorqueValues.Add(new formMeasurementProperties.ValuePoint(time, torque));
                    Biodex_Client.FormMeasurementProperties.ChartValuesVelocityValues.Add(new formMeasurementProperties.ValuePoint(time, velocity));
                    Biodex_Client.FormMeasurementProperties.ChartValuesAngleValues.Add(new formMeasurementProperties.ValuePoint(time, angle));

                }
            }
            catch (TimeoutException timeoutException)
            {
                System.Console.WriteLine("Timeout during read " + timeoutException.Message);
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
            catch (Exception SerialPortCloseException)
            {
                System.Console.WriteLine("Serial Port could not be closed " + SerialPortCloseException.Message);
            }
        }
    }
}
