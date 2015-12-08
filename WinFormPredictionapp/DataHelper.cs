using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;


namespace WinFormPredictionapp
{
    public static class DataHelper
    {
        //Получение данных из JSON, запись в словарь. Данные берутся по ссылке, указанной в QueryCommand.cs
        public static IDictionary<string,string> GetDataFromJson()
        {
            IDictionary<string, string> dataSet = new SortedDictionary<string, string>();
            using (var w = new WebClient())
            {
                dynamic dataJson;
               
                try
                {                 
                     dataJson = JObject.Parse(w.DownloadString(QueryCommands.UrlUSDEURCurrencyJSON));
                     var column_names = dataJson.dataset.column_names;
                     foreach (var item in dataJson.dataset.data)
                     {
                        dataSet.Add(item.First.Value.ToString(), item.Last.Value.ToString());
                     }                                           
                }
                catch(Exception e)
                {
                    throw e;                   
                }

                return dataSet;
            }
        }

        //Из полученного выше словаря из JSON формируем большой insert - запрос :)
        public static string GenerateInsertScriptForDataSet(IDictionary<string, string> dataSet, string tableName, string dbName, int stepsBack = -1)
        {        
            string result = "";

            int dataSetCount = dataSet.Count;

            foreach (var item in dataSet)
            {
                if (dataSetCount < stepsBack)
                    break;
                result += String.Format("INSERT INTO [dbo].[{0}] VALUES ('{1}',{2}) \n", tableName, item.Key, item.Value);
                dataSetCount--;
            }

            return result;
        }


        //
        public static List<DataPoint> CreateListDataPointFromDictionary(IDictionary<string, string> dict)
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


        public static decimal[] CreateDecimalArrayFromDictionary(IDictionary<string, string> dict)
        {
            return dict.Values.Select(n => Convert.ToDecimal(n)).ToArray<decimal>();
        }

        public static List<DataPoint> CreateListDataPointFromForecastTable(ForecastTable ft, IDictionary<string, string> originalDataSet, int startPredictValue)
        {
            List<DataPoint> resultList = new List<DataPoint>();

            int i = 0;
            foreach (var item in originalDataSet)
            {
                DateTime dt;
                if (!DateTime.TryParse(item.Key, out dt))
                    throw new ArgumentException("Плохое время");
                resultList.Add(new DataPoint(DateTimeAxis.ToDouble(dt), Convert.ToDouble(item.Value)));
                if (++i == startPredictValue)
                    break;

            }

            //теперь берем дату из словаря, а значение из прогноза
            for (; i < originalDataSet.Keys.Count; ++i)
            {
                DateTime dt;
                if (!DateTime.TryParse(originalDataSet.Keys.ElementAt<string>(i), out dt))
                    throw new ArgumentException("Плохое время");
                resultList.Add(new DataPoint(DateTimeAxis.ToDouble(dt), Convert.ToDouble(ft.Rows[i][1])));
            }
            //Новые значения
            for (i = originalDataSet.Keys.Count; i < ft.Rows.Count; ++i)
            {
                resultList.Add(new DataPoint(resultList.Last().X + 1, Convert.ToDouble(ft.Rows[i][2])));

            }

            return resultList;
        }



    }
}
