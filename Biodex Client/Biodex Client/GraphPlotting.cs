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
        formGraphs m_aFormGraphs = null;
        public ChartValues<ValuePoint> m_aChartValuesTorqueValues { get; set; }
        public ChartValues<ValuePoint> m_aChartValuesVelocityValues { get; set; }
        public ChartValues<ValuePoint> m_aChartValuesAngleValues { get; set; }

        public GraphPlotting(formGraphs aFormGraphs)
        {
            m_aFormGraphs = aFormGraphs;
            

            var mapper = Mappers.Xy<ValuePoint>()
                 .X(model => model.m_nFrame)
                 .Y(model => model.m_nValue);

            Charting.For<ValuePoint>(mapper);

            m_aChartValuesTorqueValues = new ChartValues<ValuePoint>();
            m_aChartValuesVelocityValues = new ChartValues<ValuePoint>();
            m_aChartValuesAngleValues = new ChartValues<ValuePoint>();


            //chartTorqueInitialisation
            m_aFormGraphs.chartTorque.DisableAnimations = true;
            m_aFormGraphs.chartTorque.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Frames",
                //DisableAnimations = true,
                //MaxValue = graphData.Time.Length
            });

            m_aFormGraphs.chartTorque.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Torque",
                //DisableAnimations = true,
                //MaxValue = 600, 
                //MinValue=400
            });



            //chartVelocityInitialisation
            m_aFormGraphs.chartVelocity.DisableAnimations = true;
            m_aFormGraphs.chartVelocity.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Frames",
                //DisableAnimations = true,
                //MaxValue = graphData.Time.Length
            });

            m_aFormGraphs.chartVelocity.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Angular Velocity",
                //DisableAnimations = true,
                //MaxValue = 600, 
                //MinValue=400
            });



            //chartAngleInitialisation
            m_aFormGraphs.chartAngle.DisableAnimations = true;
            m_aFormGraphs.chartAngle.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Frames",
                //DisableAnimations = true,
                //MaxValue = graphData.Time.Length
            });

            m_aFormGraphs.chartAngle.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Angle",
                //DisableAnimations = true,
                //MaxValue = 600, 
                //MinValue=400
            });
        }

        public void refreshCharts()
        {
            m_aChartValuesTorqueValues.Clear();
            m_aChartValuesVelocityValues.Clear();
            m_aChartValuesAngleValues.Clear();

            SeriesCollection helperTorqueSeries = new SeriesCollection
            {
                    new LineSeries
                    {
                        Values=m_aChartValuesTorqueValues,
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
                        Values=m_aChartValuesVelocityValues,
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
                        Values=m_aChartValuesAngleValues,
                        Title="Angle",
                        StrokeThickness = 1, //maybe change
                        PointGeometrySize = 0,
                        Stroke=System.Windows.Media.Brushes.Khaki, //lightskyblue, tomato, khaki
                        Fill=System.Windows.Media.Brushes.Transparent
                    }
            };

            m_aFormGraphs.chartTorque.Series = helperTorqueSeries;
            m_aFormGraphs.chartVelocity.Series = helperVelocitySeries;
            m_aFormGraphs.chartAngle.Series = helperAngleSeries;

        }

    }

    public class ValuePoint
    {
        public double m_nFrame { get; }
        public double m_nValue { get; }

        public ValuePoint(double nFrame, double nValue)
        {
            m_nFrame = nFrame;
            m_nValue = nValue;
        }
    }
}
