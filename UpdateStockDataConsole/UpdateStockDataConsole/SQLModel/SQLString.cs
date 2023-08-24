using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateStockDataConsole.SQLModel
{
    internal class SQLString
    {
        static string InsertStockData_SQLString()
        {
            

            string SqlStr = $@"
            INSERT INTO StockDataTable
            (RecordDate
            ,DealStocks
            ,DealPrice
            ,OpenningPrice
            ,ClosingPrice
            ,HighestPrice
            ,LowestPrice
            ,PriceDiff
            ,DealNumber
            ,StockNo)
            VALUES
            (@RecordDate
            ,@DealStocks
            ,@DealPrice
            ,@OpenningPrice
            ,@ClosingPrice
            ,@HighestPrice
            ,@LowestPrice
            ,@PriceDiff
            ,@DealNumber
            ,@StockNo)
            ";
            return SqlStr;
        }


    }
}
