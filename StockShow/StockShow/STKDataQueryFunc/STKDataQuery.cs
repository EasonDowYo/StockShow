using System.Data;

namespace StockShow.STKDataQueryFunc
{
    public class STKDataQuery
    {
        public DataTable GetStockNoData(string _stockNo,int days)
        {
            
            string sqlstr = $@"SELECT TOP({days}) * FROM Stock_db.dbo.StockDataTable WHERE StockNo='{_stockNo}' ORDER BY RecordDate DESC";
            string whole_sql = $@"SELECT * FROM ({sqlstr}) AS A ORDER BY [RecordDate] ASC";
            DataTable dt = new DataTable();

            using (SQLHandler.DBConnect dbConn = new SQLHandler.DBConnect())
            {
                dbConn.SelectDB = SQLHandler.DBServer.DatabaseList.StockDB;

                dt = dbConn.DoQuery(whole_sql);
            }

            return dt;
        }

        public DataTable GetDataByDays(string _stockNo,int days)
        {
            string sqlstr = $@" SELECT * FROM
                                    (SELECT TOP({days}) * FROM Stock_db.dbo.StockDataTable WHERE StockNo='{_stockNo}' ORDER BY RecordDate DESC) AS A
                                ORDER BY RecordDate";
            DataTable dt = new DataTable();

            using (SQLHandler.DBConnect dbConn = new SQLHandler.DBConnect())
            {
                dbConn.SelectDB = SQLHandler.DBServer.DatabaseList.StockDB;


                dt = dbConn.DoQuery(sqlstr);
            }

            return dt;
        }

        public DataTable Get5DaysData(string _stockNo)
        {
            string sqlstr = $@"SELECT * FROM
(SELECT TOP(119) * FROM Stock_db.dbo.StockDataTable WHERE StockNo='2330' ORDER BY RecordDate DESC) AS A

ORDER BY RecordDate";
            DataTable dt = new DataTable();

            using (SQLHandler.DBConnect dbConn = new SQLHandler.DBConnect())
            {
                dbConn.SelectDB = SQLHandler.DBServer.DatabaseList.StockDB;
                

                dt=dbConn.DoQuery(sqlstr);
            }

            return dt;
        }

        public DataTable Get15DaysData(string _stockNo)
        {
            string sqlstr = $@"SELECT TOP(15) * FROM Stock_db.dbo.StockDataTable WHERE StockNo='{_stockNo}' ORDER BY RecordDate DESC";
            DataTable dt = new DataTable();

            using (SQLHandler.DBConnect dbConn = new SQLHandler.DBConnect())
            {
                dbConn.SelectDB = SQLHandler.DBServer.DatabaseList.StockDB;


                dt = dbConn.DoQuery(sqlstr);
            }

            return dt;
        }

        public DataTable Get30DaysData(string _stockNo)
        {
            string sqlstr = $@"SELECT TOP(30) * FROM Stock_db.dbo.StockDataTable WHERE StockNo='{_stockNo}' ORDER BY RecordDate DESC";
            DataTable dt = new DataTable();

            using (SQLHandler.DBConnect dbConn = new SQLHandler.DBConnect())
            {
                dbConn.SelectDB = SQLHandler.DBServer.DatabaseList.StockDB;


                dt = dbConn.DoQuery(sqlstr);
            }

            return dt;
        }
    }
}
