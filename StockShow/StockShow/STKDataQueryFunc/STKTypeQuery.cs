using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace StockShow.STKDataQueryFunc
{
    public class STKTypeQuery
    {
        static public List<SelectListItem> GetAllSTKType()
        {
            try
            {
                string sqlstr = $@"SELECT * FROM [Stock_db].[dbo].[StockTypeTable]";
                DataTable dt = new DataTable();

                using (SQLHandler.DBConnect dbConn = new SQLHandler.DBConnect())
                {
                    dbConn.SelectDB = SQLHandler.DBServer.DatabaseList.StockDB;
                    dt = dbConn.DoQuery(sqlstr);
                }

                List<SelectListItem> typeList = new List<SelectListItem>();

                for(int i=0;i<dt.Rows.Count;i++)
                {
                    SelectListItem item = new SelectListItem();
                    item.Text = dt.Rows[i]["StockType"].ToString();
                    item.Value = dt.Rows[i]["StockTypeIndex"].ToString();
                    typeList.Add(item);
                }

                return typeList;
            }
            catch(Exception ex)
            {
                return new List<SelectListItem>();
            }
            
        }

    }
}
