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
using System.Threading;

namespace Biodex_Client
{
    public partial class formMicrocontrollerStatus : Form
    {
        public formMicrocontrollerStatus(Settings settings)
        {
            InitializeComponent();

            this.settings = settings;

            string[] aCOMPortsArray = SerialPort.GetPortNames();

            for (int i = 0; i < aCOMPortsArray.Length; i++)
            {
                cbxSerialPort.Items.Add(aCOMPortsArray[i]);
            }
        }

        private Settings settings;

        private void cbxSerialPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.sSerialPort = cbxSerialPort.SelectedItem.ToString();
            TestConnection();
        }

        /*
         * Serial Port Connection is tested
         */
        private void TestConnection()
        {
            try
            {
                SerialPort TestConnectionSerialPort = new SerialPort(settings.sSerialPort);
                TestConnectionSerialPort.Open();
                Thread.Sleep(1000);
                TestConnectionSerialPort.Close();
                tbConnection.BackColor = Color.Green;
            }
            catch (Exception ConnectionFailedException)
            {
                System.Console.WriteLine("Serial Port could not be opened " + ConnectionFailedException.Message);
                tbConnection.BackColor = Color.Red;

            }

        }

        
    }
}
