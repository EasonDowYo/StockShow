using StockShow.Models;
using System.Data;

namespace StockShow.STKDataQuery
{
    public class StockNoQuery
    {
        public DataTable GetAllStockNo()
        {
            string sqlstr = $@"SELECT * FROM Stock_db.dbo.StockNoTable ";
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
