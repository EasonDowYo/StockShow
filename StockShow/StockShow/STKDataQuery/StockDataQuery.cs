using System.Data;

namespace StockShow.STKDataQuery
{
    public class StockDataQuery
    {

        public DataTable Get5DaysData(string _stockNo)
        {
            string sqlstr = $@"SELECT TOP(5) * FROM Stock_db.dbo.StockDataTable WHERE StockNo={_stockNo} ORDER BY RecordDate DESC";
            DataTable dt = new DataTable();

            using (SQLHandler.DBConnect dbConn = new SQLHandler.DBConnect())
            {
                dbConn.SelectDB = SQLHandler.DBServer.DatabaseList.StockDB;
                

                dt=dbConn.DoQuery(sqlstr);
            }

            return dt;
        }


    }
}
