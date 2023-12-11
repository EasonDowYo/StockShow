using Microsoft.Data.SqlClient;
using System.Data;

namespace StockShow.SQLHandler
{
    public class DBConnect : IDisposable
    {
        public SqlConnection SqlConn;

        private SqlConnectionStringBuilder ConnStrBuilder = new SqlConnectionStringBuilder();

        private DBServer.DatabaseList _selectDB;
        public DBServer.DatabaseList SelectDB
        {
            get { return _selectDB; }
            set
            {
                if (SqlConn is null || SqlConn.State == ConnectionState.Open)
                {
                    Console.Write("[DB Connect] Connection is not");
                    //SqlConn.Close();
                    //SqlConn.Dispose();
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

        public DBConnect()
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
                    case DBServer.DatabaseList.StockDB:
                        Server = $@"DESKTOP-FPU2A87\SQLEXPRESS";
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
                Console.WriteLine("[DB Connect] ConnectToDB Error : " + e.ToString());
                return false;
            }
        }
        public DataTable DoQuery(string SqlStr)
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

                //QueryResult.Load(SqlCmd.ExecuteReader());
                //QueryResult.Load(SqlCmd.ExecuteReader());
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SqlCmd);

                //int rowsAffected = SqlCmd.ExecuteNonQuery();

                sqlDataAdapter.Fill(QueryResult);
            }
            catch (Exception e)
            {
                Console.WriteLine("[DB Connect] Query Error :" + e.ToString());
            }

            return QueryResult;
        }
        public DataTable DoOnlyExecute(string SqlStr)
        {
            if (SqlConn == null)
            {
                Console.WriteLine("[DB Connect] Connect is null.");
                return new DataTable();
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
                if (rowsaffect > 0)
                {
                    Console.WriteLine("SQL command success , affect " + rowsaffect + " rows");
                }
                else
                {
                    Console.WriteLine("SQL command success , not affect rows");
                }
                //QueryResult.Load(SqlCmd.ExecuteReader());
                //QueryResult.Load(SqlCmd.ExecuteReader());
                //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SqlCmd);
                //sqlDataAdapter.Fill(QueryResult);
            }
            catch (Exception e)
            {
                Console.WriteLine("[DB Connect] Execute Error :" + e.ToString());
            }

            return QueryResult;
        }

        //參考 https://coolmandiary.blogspot.com/2020/11/cidisposable.html
        //讓類型知道自己是否已經被釋放的Flag
        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true); //執行與釋放 (Free)、釋放 (Release) 或重設 Unmanaged 資源相關聯之應用程式定義的工作。
            GC.SuppressFinalize(this); //要求CLR要為指定之物件呼叫完成項(防止呼叫多餘的垃圾收集)。
        }

        public void Close()
        {
            Dispose();
        }
        ~DBConnect()
        {
            //必須為false
            Dispose(false);
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
