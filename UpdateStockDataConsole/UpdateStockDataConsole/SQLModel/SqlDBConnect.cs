using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace UpdateStockDataConsole.SQLModel
{
    internal class SqlDBConnect :IDisposable
    {

        public SqlConnection SqlConn;

        private SqlConnectionStringBuilder ConnStrBuilder = new SqlConnectionStringBuilder();

        private DatabaseServer.DatabaseList _selectDB;
        public DatabaseServer.DatabaseList SelectDB
        {
            get { return _selectDB; }
            set
            {
                if(SqlConn==null || SqlConn.State == ConnectionState.Open)
                {
                    Console.Write("[DB Connect] Connection is not");
                    SqlConn.Close();
                    SqlConn.Dispose();
                }
                _selectDB = value;
                if (ConnectToDB())
                {
                    Console.WriteLine("[DB Connect] Connect Sucess");
                }
                else
                {
                    Console.WriteLine("[DB Connect] Connect Fail");
                }
            }
        }

        public SqlDBConnect()
        {
            SqlConn = new SqlConnection();
        }
        private bool ConnectToDB()
        {
            try
            {
                
                string UserID = "";
                string Server = "";
                string Database = "";
                switch (_selectDB)
                {
                    case DatabaseServer.DatabaseList.StockDB:
                        Server = $@".\SQLEXPRESS";
                        Database = "Stock_db";
                        //ConnStrBuilder.UserID = "YuEason";
                        //ConnStrBuilder.Password = "";
                        //ConnStrBuilder.InitialCatalog = "Stock_db";
                        //ConnStrBuilder.TrustServerCertificate = true;

                        break;
                    default:
                        Console.WriteLine("[DB Connect] No DB Selected");
                        return false;
                        
                }

                string connString = $@"Server={Server};Database={Database};Trusted_Connection=True;MultipleActiveResultSets=true;";
                //Console.WriteLine(ConnStrBuilder.ConnectionString);
                //SqlConn = new SqlConnection("Server=.\\sqlexpress;Database=Stock_db;Trusted_Connection=True;MultipleActiveResultSets=true;");
                SqlConn = new SqlConnection(connString);
                SqlConn.Open();
                if (SqlConn.State != ConnectionState.Open)
                    Console.WriteLine("[DB Connect] Connect MSSQL open failed");

                return true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("[DB Connect] ConnectToDB Error : "+e.ToString());
                return false;
            }
        }
        public DataTable DoQuery(string SqlStr)
        {
            if (SqlConn == null)
            {
                Console.WriteLine("[DB Connect] Connect is null.");
            }
            if(SqlConn.State != ConnectionState.Open)
            {
                Console.WriteLine("[DB Connect] Connect does not open.");
            }

            DataTable QueryResult = new DataTable();

            try
            {
                SqlCommand SqlCmd = new SqlCommand(SqlStr, SqlConn);
                SqlCmd.CommandTimeout = 60;

                //QueryResult.Load(SqlCmd.ExecuteReader());
                //QueryResult.Load(SqlCmd.ExecuteReader());
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SqlCmd);

                //int rowsAffected = SqlCmd.ExecuteNonQuery();

                sqlDataAdapter.Fill(QueryResult);
            }
            catch(Exception e)
            {
                Console.WriteLine("[DB Connect] Query Error :"+e.ToString());
            }

            return QueryResult;
        }
        public DataTable DoExecute(string SqlStr)
        {
            if (SqlConn == null)
            {
                Console.WriteLine("[DB Connect] Connect is null.");
            }
            if (SqlConn.State != ConnectionState.Open)
            {
                Console.WriteLine("[DB Connect] Connect does not open.");
            }

            DataTable QueryResult = new DataTable();

            try
            {
                SqlCommand SqlCmd = new SqlCommand(SqlStr, SqlConn);
                SqlCmd.CommandTimeout = 60;
                int rowsaffect = SqlCmd.ExecuteNonQuery();
                //QueryResult.Load(SqlCmd.ExecuteReader());
                //QueryResult.Load(SqlCmd.ExecuteReader());
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SqlCmd);
                sqlDataAdapter.Fill(QueryResult);
            }
            catch (Exception e)
            {
                Console.WriteLine("[DB Connect] Execute Error :" + e.ToString());
            }

            return QueryResult;
        }


        private bool disposed = false;
        // 實現IDisposable接口
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // 釋放資源
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // 釋放托管資源
                    
                }

                // 釋放非托管資源
                SqlConn.Close();
                SqlConn.Dispose();
                disposed = true;
            }
        }

    }
}
