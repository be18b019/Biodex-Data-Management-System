using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biodex_Client
{
    /*for liveplotting lists are needed which receive values eachtime 
    the serialdataeventhandler fires
    these lists need to be then converted into the already existing 
    double arrays*/
    public class Data
    {
        //Calibration Coefficients (FROM MATLAB_GUI)
        const double TORQUE_CAL_FACTOR = 1.04;
        const double TORQUE_CAL_SUBTRAHEND = -552;
        const double VELOCITY_CAL_FACTOR = 0.883;
        const double VELOCITY_CAL_SUBTRAHEND = -459;
        const double ANGLE_CAL_FACTOR = 0.363;
        const double ANGLE_CAL_SUBTRAHEND = -99.3;

        //Measurment Properties + Calibrated Measurement data
        //public double[] Time { get; set; }
        //public double[] Torque { get; set; }
        //public double[] Velocity { get; set; }
        //public double[] Angle { get; set; }
        //public Data GraphDataCalibrated { get; set; }

        //Measurment Properties as Lists
        public List<double> aTimeList { get; set; }
        public List<double> aTorqueList { get; set; }
        public List<double> aVelocityList { get; set; }
        public List<double> aAngleList { get; set; }

        public Data()
        {
            aTimeList = new List<double>();
            aTorqueList = new List<double>();
            aVelocityList = new List<double>();
            aAngleList = new List<double>();
        }

        //public Data(int[][] data) : this()
        //{
        //    Time = createTimeProperty(data, Time);
        //    Torque = getDatainClassProperty(data, Torque, 0);
        //    Velocity = getDatainClassProperty(data, Velocity, 1);
        //    Angle = getDatainClassProperty(data, Angle, 2);
        //    GraphDataCalibrated = calibrateData(data, Time, Torque, Velocity, Angle);
        //}

        /*
        * adds single data values to the lists
        */
        public void AddtoLists(double Torque, double Velocity, double Angle)
        {
            aTimeList.Add(aTimeList.Count + 1);
            aTorqueList.Add(Torque * TORQUE_CAL_FACTOR + TORQUE_CAL_SUBTRAHEND);
            aVelocityList.Add(Velocity * VELOCITY_CAL_FACTOR + VELOCITY_CAL_SUBTRAHEND);
            aAngleList.Add(Angle * ANGLE_CAL_FACTOR + ANGLE_CAL_SUBTRAHEND);
        }

        //get data from DB and plot them
        public Data(string Torque, string Velocity, string Angle):this()
        {
			try
			{
                Biodex_Client.GraphPlotting.refreshCharts();

                aTorqueList = Array.ConvertAll(Torque.Split(';'), double.Parse).ToList();
                aVelocityList = Array.ConvertAll(Velocity.Split(';'), double.Parse).ToList();
                aAngleList = Array.ConvertAll(Angle.Split(';'), double.Parse).ToList();


                for (int i = 0; i < aTorqueList.Count; i++)
                {
                    aTimeList.Add(aTimeList.Count + 1);

                    Biodex_Client.GraphPlotting.m_aChartValuesTorqueValues.Add(new ValuePoint(aTimeList[i], aTorqueList[i]));
                    Biodex_Client.GraphPlotting.m_aChartValuesVelocityValues.Add(new ValuePoint(aTimeList[i], aVelocityList[i]));
                    Biodex_Client.GraphPlotting.m_aChartValuesAngleValues.Add(new ValuePoint(aTimeList[i], aAngleList[i]));
                }

                MessageBox.Show("Loaded Former Records From Database Successfully and PLOTTED THEM. Now You Can Also Create a CSV-File", "Loading Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
			catch (Exception)
			{
                MessageBox.Show("The Record Can NOT BE PLOTTED. HINT: The Lists Torque, Velocity or Angle are EMPTY", "CANNOT PLOT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
		}

        /*
         * clears data from alle lists
         */
        public void ClearLists()
        {
            aTimeList.Clear();
            aTorqueList.Clear();
            aVelocityList.Clear();
            aAngleList.Clear();
        }

        /*
         * generates the calibrated data object 
         */
        //Data calibrateData(int[][] data, double[] time, double[] torque, double[] velocity, double[] angle)
        //{
        //    Data graphData = new Data();
        //    graphData.Time = time;
        //    graphData.Torque = torque;
        //    graphData.Velocity = velocity;
        //    graphData.Angle = angle;

        //    for (int i = 0; i < data[0].Length; i++)
        //    {
        //        graphData.Torque[i] = graphData.Torque[i] * TORQUE_CAL_FACTOR + TORQUE_CAL_SUBTRAHEND;
        //        graphData.Velocity[i] = graphData.Velocity[i] * VELOCITY_CAL_FACTOR + VELOCITY_CAL_SUBTRAHEND;
        //        graphData.Angle[i] = graphData.Angle[i] * ANGLE_CAL_FACTOR + ANGLE_CAL_SUBTRAHEND;
        //    }
        //    return graphData;
        //}

        /*
         * arranges data from data array into data object properties
         * will be removed as loadbutton and serialport are available
         */
        //double[] getDatainClassProperty(int[][] data, double[] classData, int index)
        //{
        //    double[] temp = new double[data[0].Length];
        //    for (int i = 0; i < data[0].Length; i++)
        //    {
        //        temp[i] = Convert.ToDouble(data[index][i]);
        //    }
        //    classData = temp;
        //    return classData;
        //}

        /*
         * generates the data object time/frames property
         * will be changed when loadbutton and serialport are implemented
         */
    //    double[] createTimeProperty(int[][] data, double[] classData)
    //    {
    //        double[] temp = new double[data[0].Length];
    //        for (int i = 0; i < data[0].Length; i++)
    //        {

    //            temp[i] = i;
    //        }
    //        classData = temp;
    //        return classData;
    //    }
    }
}
