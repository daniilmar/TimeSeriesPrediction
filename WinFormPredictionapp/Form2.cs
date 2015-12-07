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

namespace WinFormPredictionapp
{
    public partial class Form2 : Form
    {
        public Form2()
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



            //Период - сколько пред. знач учитываем при высчите нового
            //Holdout - количество чисел с конца из сущест. датасета, у которых берется VALUE для прогноза последующих точек, а не forecast-value
            
            var result = TimeSeries.simpleMovingAverage(CreateDecimalArrayFromDictionary(dataSet), 150, 100, dataSet.Count );

            var secondList = CreateListDataPointFromForecastTable(result, dataSet, dataSet.Keys.Count-1000);
            CreatePlot(CreateListDataPointFromDictionary(dataSet), secondList);
            #endregion

        }
        private List<DataPoint> CreateListDataPointFromDictionary(IDictionary<string, string> dict)
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


        private decimal[] CreateDecimalArrayFromDictionary(IDictionary<string,string> dict)
        {
            return dict.Values.Select(n => Convert.ToDecimal(n)).ToArray<decimal>();
        }

        private List<DataPoint> CreateListDataPointFromForecastTable(ForecastTable ft, IDictionary<string,string> originalDataSet, int startPredictValue)
        {
            List<DataPoint> resultList = new List<DataPoint>();

            int i = 0;
            foreach(var item in originalDataSet)
            {
                DateTime dt;
                if (!DateTime.TryParse(item.Key, out dt))
                    throw new ArgumentException("Плохое время");
                resultList.Add(new DataPoint(DateTimeAxis.ToDouble(dt), Convert.ToDouble(item.Value)));
                if (++i == startPredictValue)
                    break;
                
            }

            //теперь берем дату из словаря, а значение из прогноза
            for(;i<originalDataSet.Keys.Count; ++i)
            {
                DateTime dt;
                if (!DateTime.TryParse(originalDataSet.Keys.ElementAt<string>(i), out dt))
                    throw new ArgumentException("Плохое время");
                resultList.Add(new DataPoint(DateTimeAxis.ToDouble(dt), Convert.ToDouble(ft.Rows[i][1])));
            }
            //Новые значения
            for(i=originalDataSet.Keys.Count; i<ft.Rows.Count; ++i)
            {
                resultList.Add(new DataPoint(resultList.Last().X + 1, Convert.ToDouble(ft.Rows[i][2])));
                
            }

            return  resultList;
        }


        private void CreatePlot(List<DataPoint> pointList, List<DataPoint> predictionList)
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

            var s2 = new LineSeries();
            foreach (var point in predictionList)
                s2.Points.Add(point);


            s2.Color = OxyColor.Parse("#0000FF");

            pm.Series.Add(s2);

            var s1 = new LineSeries();
            s1.Color = OxyColor.Parse("#FF0000");
            foreach (var point in pointList)
                s1.Points.Add(point);
            pm.Series.Add(s1);


            plot1.Model = pm;
        }

        private void label1_Click(object sender, EventArgs e)
        {

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
