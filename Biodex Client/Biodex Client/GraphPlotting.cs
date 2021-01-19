using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;

namespace Biodex_Client
{
    public class GraphPlotting
    {
        formGraphs _FormGraphs = null;
        public ChartValues<ValuePoint> ChartValuesTorqueValues { get; set; }
        public ChartValues<ValuePoint> ChartValuesVelocityValues { get; set; }
        public ChartValues<ValuePoint> ChartValuesAngleValues { get; set; }

        public GraphPlotting(formGraphs FormGraphs)
        {
            _FormGraphs = FormGraphs;
            

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
        }

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

    }

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
}
