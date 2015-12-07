using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using Microsoft.AnalysisServices.AdomdClient;
using System.Data.SqlClient;
using RDotNet;


namespace WinFormPredictionapp
{

    public class BIGetter
    {




    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            IDictionary<string, string> dataSet = DataHelper.GetDataFromJson();


            #region Создание бд и наполнение данными
              SqlProvider sqlProv = new SqlProvider(QueryCommands.SQLConnectionString, "CurrencyDb");
            //  sqlProv.CreateDB();
            // sqlProv.CreateTable("Currency");
            //    sqlProv.ExecuteQuery(DataHelper.GenerateInsertScriptForDataSet(dataSet, "Currency", sqlProv.DBName));
            #endregion

            #region Создание таблицы, на основе которой создадим модель прогноза
           // sqlProv.CreateTable("CurrencyForPrediction");
            //sqlProv.ExecuteQuery(DataHelper.GenerateInsertScriptForDataSet(dataSet, "CurrencyForPrediction", sqlProv.DBName, 50));
            #endregion
            #region drawCode
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            var result = TimeSeries.weightedMovingAverage(new decimal[] { 1, 2, 3, 4 }, 2, 2);

            

           // CreatePlot(CreateListDataPointFromDictionary(dataSet));
            #endregion

          
        }


       private List<DataPoint> CreateListDataPointFromDictionary(IDictionary<string,string> dict)
        {
            List<DataPoint> resultList = new List<DataPoint>();

            foreach (var item in dict)
            {
                DateTime dt;
                if (!DateTime.TryParse(item.Key, out dt))
                    throw new ArgumentException("Плохое время");
                resultList.Add(new DataPoint(DateTimeAxis.ToDouble(dt), Convert.ToDouble(item.Value)));
            }

            return resultList;
        }


        private void CreatePlot(List<DataPoint> pointList)
        {
            var pm = new PlotModel
            {
                Title = "EUR/USD",
                PlotType = PlotType.Cartesian,
                Background = OxyColors.White
            };

            var xAxis = new DateTimeAxis();
            var yAxis = new LinearAxis();

            yAxis.AbsoluteMinimum = 0.5;
            yAxis.AbsoluteMaximum = 2;

            pm.Axes.Add(xAxis);
            pm.Axes.Add(yAxis);

            var s1 = new LineSeries();
            foreach (var point in pointList)
                s1.Points.Add(point);

            pm.Series.Add(s1);
            plot1.Model = pm;
        }

        /*
        private void CreatePlot(List<SortedList<int,DataPoint>> listList)
        {
            var pm = new PlotModel
            {
                Title = "Alcogol demand time series",
                Subtitle = "Example using the FunctionSeries",
                PlotType = PlotType.Cartesian,
                Background = OxyColors.White              
            };

            var yAxis = new LinearAxis();
            
            yAxis.AbsoluteMinimum = 0.5;
            yAxis.AbsoluteMaximum = 2;

            pm.Axes.Add(yAxis);


            int count = 0;
            
            foreach (var pointList in listList)
            {
                
                var s1 = new LineSeries();
                if (count == 0)
                    s1.Color = OxyColor.Parse("#FF0000");
                else
                    s1.Color = OxyColor.Parse("#0000FF");

                foreach(var point in pointList)
                    s1.Points.Add(point.Value);

                pm.Series.Add(s1);
                count = 1;
            }
                
            plot1.Model = pm;
            
        }
        */
    }
}
