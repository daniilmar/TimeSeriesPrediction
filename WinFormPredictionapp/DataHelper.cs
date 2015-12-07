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


namespace WinFormPredictionapp
{
    public static class DataHelper
    {
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

       
    }
}
