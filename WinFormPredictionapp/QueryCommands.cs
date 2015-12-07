using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormPredictionapp
{
    public static class QueryCommands
    {
        public static string UrlUSDEURCurrencyJSON = "https://www.quandl.com/api/v3/datasets/ECB/EURUSD.json";


        public static string CreateMiningModelQuery =
            @"CREATE MINING MODEL [Forecasting_USDEUR]
            (
                [Date] DATE KEY TIME,
                [Value] DOUBLE CONTINUOUS PREDICT
            )
                USING Microsoft_Time_Series (AUTO_DETECT_PERIODICITY = 0.8, FORECAST_METHOD = 'MIXED')
                WITH DRILLTHROUGH";



        //до 1938
        public static string SelectOriginalDemandDataCommand = "Select Date as Date , Value as Value from dbo.Demand";

        //до 1929
        public static string SelectOriginalDemandDataForTeachingPredictModel = "Select Date as Date , Value as Value from dbo.Demand_Original";

        public static string ADOMDConnectionString = "Provider=SQLNCLI11.1;Data Source=localhost;Integrated Security=SSPI;Initial Catalog=TimeSeriesPredd";

        public static string SQLConnectionString = "Data Source = localhost; Integrated Security = SSPI; MultipleActiveResultSets=True";


    }
}
