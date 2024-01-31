using Microsoft.AspNetCore.Mvc.Rendering;
using StockShow.Models;
using System.Data;

namespace StockShow.STKDataQuery
{
    public class STKNoQuery
    {
        static public List<SelectListItem> GetAllStockNo()
        {
            try
            {
                List<SelectListItem> STKNoList = new List<SelectListItem>();
                string sqlstr = $@"SELECT * FROM Stock_db.dbo.StockNoTable ";
                DataTable dt = new DataTable();

                using (SQLHandler.DBConnect dbConn = new SQLHandler.DBConnect())
                {
                    dbConn.SelectDB = SQLHandler.DBServer.DatabaseList.StockDB;

                    dt = dbConn.DoQuery(sqlstr);
                }

                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    SelectListItem item = new SelectListItem();
                    item.Text = dt.Rows[i]["StockName"].ToString();
                    item.Value = dt.Rows[i]["StockNo"].ToString();
                    STKNoList.Add(item);
                }
                return STKNoList;
            }
            catch
            {
                return new List<SelectListItem>();
            }

        }
        static public List<SelectListItem> GetStockNo_BySTKTypeIndex(int _stockTypeIndex)
        {
            try
            {
                List<SelectListItem> STKNoList = new List<SelectListItem>();
                string sqlstr = $@"SELECT * FROM Stock_db.dbo.StockNoTable WHERE StockTypeIndex={_stockTypeIndex}";
                DataTable dt = new DataTable();

                using (SQLHandler.DBConnect dbConn = new SQLHandler.DBConnect())
                {
                    dbConn.SelectDB = SQLHandler.DBServer.DatabaseList.StockDB;

                    dt = dbConn.DoQuery(sqlstr);
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SelectListItem item = new SelectListItem();
                    item.Text = dt.Rows[i]["StockName"].ToString();
                    item.Value = dt.Rows[i]["StockNo"].ToString();
                    STKNoList.Add(item);
                }
                return STKNoList;
            }
            catch
            {
                return new List<SelectListItem>();
            }

        }

    }
}
