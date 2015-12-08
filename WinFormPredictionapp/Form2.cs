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

        IDictionary<string, string> dataSet;
        PlotModel plotModel;
        public Form2()
        {
            InitializeComponent();

            this.dataSet = DataHelper.GetDataFromJson();

            comboBoxDateStart.FormatString = "YYYY-MM-DD";
            comboBoxDateStart.DataSource = dataSet.Keys.ToList<String>();
            comboBoxDateStart.DisplayMember = "Начальная дата";


            comboBoxDateEnd.FormatString = "YYYY-MM-DD";
            comboBoxDateEnd.DataSource = dataSet.Keys.ToList<String>();
            comboBoxDateEnd.DisplayMember = "Конечная дата";

            #region Создание бд и наполнение данными
         //   SqlProvider sqlProv = new SqlProvider(QueryCommands.SQLConnectionString, "CurrencyDb");
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
            
     //       var result = TimeSeries.simpleMovingAverage(DataHelper.CreateDecimalArrayFromDictionary(dataSet), 150, 100, dataSet.Count );
        
       //     var secondList = DataHelper.CreateListDataPointFromForecastTable(result, dataSet, dataSet.Keys.Count-1000);

            InitPlotModel();


         
            #endregion

        }



        private void InitPlotModel()
        {
            plotModel = new PlotModel
            {
                Title = "EUR/USD",
                PlotType = PlotType.Cartesian,
                Background = OxyColors.White
            };

            var xAxis = new DateTimeAxis();
            var yAxis = new LinearAxis();

            yAxis.AbsoluteMinimum = 0.5;
            yAxis.AbsoluteMaximum = 2;

            plotModel.Axes.Add(xAxis);
            plotModel.Axes.Add(yAxis);

            this.plot1.Model = plotModel;

        }

        private void AddSeries(List<DataPoint> pointList)
        {
            var plotView = this.Controls.Find("plot1", false).Cast<OxyPlot.WindowsForms.PlotView>().FirstOrDefault();
            var s1 = new LineSeries();
            s1.Color = OxyColor.Parse("#FF0000");
            foreach (var point in pointList)
                s1.Points.Add(point);

            plotModel.Series.Add(s1);
            plotView.Model = plotModel;

            plotView.Model.InvalidatePlot(true);
            plotView.InvalidatePlot(true);


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Отрисовываем график основной
            AddSeries(DataHelper.CreateListDataPointFromDictionary(dataSet));
            

            //  this.plot1.Refresh();



        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
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
