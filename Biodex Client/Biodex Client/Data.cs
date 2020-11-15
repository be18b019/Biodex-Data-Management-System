using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client
{
    /*for liveplotting lists are needed which receive values eachtime 
    the serialdataeventhandler fires
    these lists need to be then converted into the already existing 
    double arrays*/
    public class Data
    {
            //Calibration Coefficients (FROM MATLAB_GUI)
            const double TORQUECALFACTOR = 1.04;
            const double TORQUECALSUBTRAHEND = -552;
            const double VELOCITYCALFACTOR = 0.883;
            const double VELOCITYCALSUBTRAHEND = -459;
            const double ANGLECALFACTOR = 0.363;
            const double ANGLECALSUBTRAHEND = -99.3;

            //Measurment Properties + Calibrated Measurement data
            public double[] Time { get; set; }
            public double[] Torque { get; set; }
            public double[] Velocity { get; set; }
            public double[] Angle { get; set; }
            public Data GraphDataCalibrated { get; set; }

            public Data() { }

            public Data(int[][] data)
            {
                Time = createTimeProperty(data, Time);
                Torque = getDatainClassProperty(data, Torque, 0);
                Velocity = getDatainClassProperty(data, Velocity, 1);
                Angle = getDatainClassProperty(data, Angle, 2);
                GraphDataCalibrated = calibrateData(data, Time, Torque, Velocity, Angle);
            }

        /*
         * generates the calibrated data object 
         */
        Data calibrateData(int[][] data, double[] time, double[] torque, double[] velocity, double[] angle)
            {
                Data graphData = new Data();
                graphData.Time = time;
                graphData.Torque = torque;
                graphData.Velocity = velocity;
                graphData.Angle = angle;

                for (int i = 0; i < data[0].Length; i++)
                {
                    graphData.Torque[i] = graphData.Torque[i] * TORQUECALFACTOR + TORQUECALSUBTRAHEND;
                    graphData.Velocity[i] = graphData.Velocity[i] * VELOCITYCALFACTOR + VELOCITYCALSUBTRAHEND;
                    graphData.Angle[i] = graphData.Angle[i] * ANGLECALFACTOR + ANGLECALSUBTRAHEND;
                }
                return graphData;
            }
            
            /*
             * arranges data from data array into data object properties
             * will be removed as loadbutton and serialport are available
             */
            double[] getDatainClassProperty(int[][] data, double[] classData, int index)
            {
                double[] temp = new double[data[0].Length];
                for (int i = 0; i < data[0].Length; i++)
                {
                    temp[i] = Convert.ToDouble(data[index][i]);
                }
                classData = temp;
                return classData;
            }

        /*
         * generates the data object time/frames property
         * will be changed when loadbutton and serialport are implemented
         */
        double[] createTimeProperty(int[][] data, double[] classData)
            {
                double[] temp = new double[data[0].Length];
                for (int i = 0; i < data[0].Length; i++)
                {

                    temp[i] = i;
                }
                classData = temp;
                return classData;
            }
    }
}
