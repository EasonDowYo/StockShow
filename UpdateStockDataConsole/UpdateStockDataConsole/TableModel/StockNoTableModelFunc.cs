using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Collections;
using UpdateStockDataConsole.SQLModel;

namespace UpdateStockDataConsole.TableModel
{
    internal class StockNoTableModelFunc
    {
        static public DataTable GetStockNoList()
        {
            //使用using需要繼承IDisposable
            using (SqlDBConnect sqlDBConnect = new SqlDBConnect())
            {
                sqlDBConnect.SelectDB = DatabaseServer.DatabaseList.StockDB;

                string sqlstring = $@"SELECT DISTINCT(StockNo) FROM [Stock_db].[dbo].[StockNoTable]";
                DataTable dataTable = sqlDBConnect.DoQuery(sqlstring);
                
                
                return dataTable;
            }
        }

    }
}
