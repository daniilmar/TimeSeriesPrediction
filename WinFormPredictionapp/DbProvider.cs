using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace WinFormPredictionapp
{
    public class SqlProvider
    {
        private string connectionString;
        private string dbName;

        public string DBName
        {
            get { return dbName; }
            set { dbName = value; }
        }
       
      

        public SqlProvider(string connectionString, string dbName)
        {
            this.connectionString = connectionString;
            this.dbName = dbName;
        }

        public void CreateDB()
        {
            string createDBCommand = string.Format("Create Database {0};", DBName);
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                SqlCommand comm = new SqlCommand(createDBCommand, conn);
                try
                {
                    conn.Open();
                    comm.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

        }

        public void ExecuteQuery(string query)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                SqlCommand comm = new SqlCommand(query, conn);
                try
                {
                    conn.Open();
                    conn.ChangeDatabase(DBName);
                    comm.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public void CreateTable(string tableName)
        {
            #region command
            string createTableCommand = 
                @"Create Table[dbo].[" + tableName +
                @"] 
                (
                    [Date] DATE NOT NULL,
                    [Value] NUMERIC(7,5) NOT NULL
                )
                ALTER TABLE[dbo].[" + tableName +
                @"]
                    ADD CONSTRAINT
                       [PK_Date"+ tableName +@"] PRIMARY KEY CLUSTERED
                       (
                           [Date]
                       );
                ";
            #endregion

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                
                SqlCommand comm = new SqlCommand(createTableCommand, conn);
                try
                {
                    conn.Open();
                    conn.ChangeDatabase(DBName);
                    comm.ExecuteNonQuery();
                }
                catch(Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
