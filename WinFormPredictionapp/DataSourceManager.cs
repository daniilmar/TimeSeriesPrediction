using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WinFormPredictionapp
{

    public interface DataSourceManager
    {
        bool GetData();

    }
    public class CSVDataSourceManager : DataSourceManager
    {
        public CSVDataSourceManager()
        { }

        public bool GetData()
        {
            return true;
        }


    }
}
