using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateStockDataConsole.SQLModel;

namespace UpdateStockDataConsole.TableModel
{
    internal class StockDataTableModelFunc
    {


        static string InsertSQLCommand()
        {

            string SqlStr = $@"
            INSERT INTO Stock_db.dbo.StockDataTable
            (RecordDate
            ,TradeVolumn
            ,TradeValue
            ,OpenningPrice
            ,ClosingPrice
            ,HighestPrice
            ,LowestPrice
            ,PriceDiff
            ,[Transaction]
            ,StockNo)
            VALUES
            (@RecordDate
            ,@TradeVolumn
            ,@TradeValue
            ,@OpenningPrice
            ,@ClosingPrice
            ,@HighestPrice
            ,@LowestPrice
            ,@PriceDiff
            ,@Transaction
            ,@StockNo)
            ";
            SqlStr = SqlStr.Replace("\r", "").Replace("\n", "");

            return SqlStr;
        }

        static public DateTime GetTheLastDateFromStock(string _stock)
        {
            //使用using需要繼承IDisposable
            using (SqlDBConnect sqlDBConnect = new SqlDBConnect())
            {

                sqlDBConnect.SelectDB = DatabaseServer.DatabaseList.StockDB;

                string sqlstring = $@"SELECT TOP(1) RecordDate FROM [Stock_db].[dbo].[StockDataTable] WHERE StockNo='{_stock}' ORDER BY RecordDate DESC";
                DataTable dataTable = sqlDBConnect.DoQuery(sqlstring);
                if (dataTable.Rows.Count == 0)
                    return DateTime.MinValue;
                DateTime lastdate = Convert.ToDateTime(dataTable.Rows[0]["RecordDate"].ToString());
                return lastdate;
            }
        }

        static public void UpdateStockDataToDate(string _stock, DateTime _startDate, DateTime _targetDate)
        {
            Console.WriteLine($@"[UpdateStockDataToDate] Start~ {_stock} !");

            // Get last date from stocNo
            DateTime stockLastUpdateDate = GetTheLastDateFromStock(_stock);


            _targetDate = _targetDate.AddDays(1 - _startDate.Day);

            //  DateTime.Compare(t1, t2) < 0  => t1 earlier t2   ,    DateTime.Compare(t1, t2) > 0  => t1 later t2
            if (DateTime.Compare(stockLastUpdateDate, _targetDate) >= 0) // If lastUpdateDate later than _targetDate
            {
                Console.WriteLine($@"{_stock} 最新的資料已經超過指定日期~ ");
                return;
            }

            if (DateTime.Compare(stockLastUpdateDate, _startDate) >= 0) // If stockLastUpdateDate later than _startDate
            {
                _startDate = stockLastUpdateDate;
                _startDate = _startDate.AddDays(1 - _startDate.Day);
            }

            try
            {
                while (true)
                {

                    if (DateTime.Compare(_startDate, _targetDate) > 0) 
                    {
                        break;
                    }
                    //DateTime tempTime = new DateTime(lastUpdateDate.Year, lastUpdateDate.Month,lastUpdateDate.Day);
                    Task<StockDataFromAPIModel?> stockDataFromAPIModel = StockAPIFunction.GetStockDataFromAPI(_stock, new DateTime(_startDate.Year, _startDate.Month, 1));
                    
                    if (stockDataFromAPIModel.Result is null)// The if condition avoid  CS8600
                    {
                        Console.WriteLine(" Call Stock API second times");
                        System.Threading.Thread.Sleep(9000);
                        stockDataFromAPIModel = StockAPIFunction.GetStockDataFromAPI(_stock, new DateTime(_startDate.Year, _startDate.Month, 1));
                        break;
                    }

                    if (stockDataFromAPIModel.Result.data is null)
                        break;

                    // Check whether each data needs to be inserted
                    for (int i = 0; i < stockDataFromAPIModel.Result.data.Count; i++)
                    {
                        DateTime tempdate = Convert.ToDateTime(stockDataFromAPIModel.Result.data[i][0].ToString()); //[i][0]第i天的日期
                        tempdate = tempdate.AddYears(1911);

                        // Compare(t1,t2)<0   t1 earlier than t2     Compare(t1,t2)>0   t1 later than t2
                        if (DateTime.Compare(tempdate, stockLastUpdateDate) > 0) // If tempdate later than stockLastUpdateDate
                        {
                            //string InsertDataTableSQLCMD = InsertSQLCommand();
                            using (SqlDBConnect sqlDBConnect = new SqlDBConnect())
                            {
                                sqlDBConnect.SelectDB = DatabaseServer.DatabaseList.StockDB;
                                SqlCommand sqlCommand = new SqlCommand(StockDataTableModelFunc.InsertSQLCommand(), sqlDBConnect.SqlConn);

                                //sqlCommand.CommandText = StockDataTableModelFunc.InsertSQLCommand();
                                sqlCommand.Parameters.AddWithValue("@RecordDate", tempdate.ToString("yyyy-MM-dd"));//   ["@RecordDate"].Value = tempdate.ToString();

                                // 成交股數
                                var temp_TradeVolumn = Convert.ToInt64(stockDataFromAPIModel.Result.data[i][1].Replace(",", "")).ToString();
                                sqlCommand.Parameters.AddWithValue("@TradeVolumn", temp_TradeVolumn);// = Convert.ToInt64(stockDataFromAPIModel.Result.data[i][1]);

                                // 成交金額
                                var temp_TradeValue = Convert.ToInt64(stockDataFromAPIModel.Result.data[i][2].Replace(",", "")).ToString();
                                sqlCommand.Parameters.AddWithValue("@TradeValue", temp_TradeValue);  // = Convert.ToInt64(stockDataFromAPIModel.Result.data[i][2]);

                                // 開盤價
                                var temp_OpenningPrice = Convert.ToDouble(stockDataFromAPIModel.Result.data[i][3]).ToString();
                                sqlCommand.Parameters.AddWithValue("@OpenningPrice", temp_OpenningPrice);  // = Convert.ToDouble(stockDataFromAPIModel.Result.data[i][3]);

                                // 最高價
                                var temp_ClosingPrice = Convert.ToDouble(stockDataFromAPIModel.Result.data[i][4]).ToString();
                                sqlCommand.Parameters.AddWithValue("@HighestPrice", temp_ClosingPrice);  //= Convert.ToDouble(stockDataFromAPIModel.Result.data[i][4]);

                                // 最低價
                                var temp_HighestPrice = Convert.ToDouble(stockDataFromAPIModel.Result.data[i][5]).ToString();
                                sqlCommand.Parameters.AddWithValue("@LowestPrice", temp_HighestPrice);  //= Convert.ToDouble(stockDataFromAPIModel.Result.data[i][5]);

                                // 收盤價
                                //var aaa = stockDataFromAPIModel.Result.data[i][6].Replace("X", "");
                                var temp_LowestPrice = Convert.ToDouble(stockDataFromAPIModel.Result.data[i][6].Replace("X", "")).ToString();
                                sqlCommand.Parameters.AddWithValue("@ClosingPrice", temp_LowestPrice);  //= Convert.ToDouble(stockDataFromAPIModel.Result.data[i][6]);

                                // 漲跌價差
                                var temp_PriceDiff = Convert.ToDouble(stockDataFromAPIModel.Result.data[i][7].Replace("X", "")).ToString();
                                sqlCommand.Parameters.AddWithValue("@PriceDiff", temp_PriceDiff);  // = Convert.ToDouble(stockDataFromAPIModel.Result.data[i][7]);

                                // 交易筆數
                                var temp_Transaction = Convert.ToInt64(stockDataFromAPIModel.Result.data[i][8].Replace(",", "")).ToString();
                                sqlCommand.Parameters.AddWithValue("@Transaction", temp_Transaction);  // = Convert.ToInt64(stockDataFromAPIModel.Result.data[i][8]);

                                sqlCommand.Parameters.AddWithValue("@StockNo", _stock);// = _stock;

                                sqlCommand.ExecuteNonQuery();

                                Console.WriteLine($@"{_stock} Date:{tempdate} Updated ");
                            }




                        }
                    }  // End -> for (int i = 0; i < stockDataFromAPIModel.Result.data.Count; i++)
                    List<string> this_round_of_data = stockDataFromAPIModel.Result.data.Last();
                    System.Threading.Thread.Sleep(9000);
                    //stockLastUpdateDate = Convert.ToDateTime(this_round_of_data[0]);
                    _startDate = _startDate.AddMonths(1);
                    //stockLastUpdateDate = stockLastUpdateDate.AddYears(1911);


                } // END while(true)
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            return;
        }

        static public void UpdateStockDataToDate_Test(string _stock, DateTime _startDate, DateTime _targetDate)
        {
            Console.WriteLine($@"[UpdateStockDataToDate] Start~ {_stock} !");
            DateTime stockLastUpdateDate = GetTheLastDateFromStock(_stock);
            //  DateTime.Compare(t1, t2) < 0  => t1 earlier t2   ,    DateTime.Compare(t1, t2) > 0  => t1 later t2
            if (DateTime.Compare(stockLastUpdateDate, _targetDate) >= 0) // If lastUpdateDate later than _targetDate
            {
                Console.WriteLine($@"{_stock} 最新的資料已經超過指定日期~ ");
                return;
            }

            try
            {

                //string InsertDataTableSQLCMD = InsertSQLCommand();
                using (SqlDBConnect sqlDBConnect = new SqlDBConnect())
                {
                    sqlDBConnect.SelectDB = DatabaseServer.DatabaseList.StockDB;
                    SqlCommand sqlCommand = new SqlCommand(StockDataTableModelFunc.InsertSQLCommand(), sqlDBConnect.SqlConn);

                    //sqlCommand.CommandText = StockDataTableModelFunc.InsertSQLCommand();
                    sqlCommand.Parameters.AddWithValue("@RecordDate", "2023-01-01");//   ["@RecordDate"].Value = tempdate.ToString();

                    // 成交股數

                    sqlCommand.Parameters.AddWithValue("@TradeVolumn", 1234);// = Convert.ToInt64(stockDataFromAPIModel.Result.data[i][1]);

                    // 成交金額

                    sqlCommand.Parameters.AddWithValue("@TradeValue", 12345);  // = Convert.ToInt64(stockDataFromAPIModel.Result.data[i][2]);

                    // 開盤價

                    sqlCommand.Parameters.AddWithValue("@OpenningPrice", 100);  // = Convert.ToDouble(stockDataFromAPIModel.Result.data[i][3]);

                    // 收盤價

                    sqlCommand.Parameters.AddWithValue("@ClosingPrice", 101);  //= Convert.ToDouble(stockDataFromAPIModel.Result.data[i][4]);

                    // 最高價

                    sqlCommand.Parameters.AddWithValue("@HighestPrice", 102);  //= Convert.ToDouble(stockDataFromAPIModel.Result.data[i][5]);

                    // 最低價

                    sqlCommand.Parameters.AddWithValue("@LowestPrice", 103);  //= Convert.ToDouble(stockDataFromAPIModel.Result.data[i][6]);

                    // 漲跌價差

                    sqlCommand.Parameters.AddWithValue("@PriceDiff", 2);  // = Convert.ToDouble(stockDataFromAPIModel.Result.data[i][7]);

                    // 交易筆數

                    sqlCommand.Parameters.AddWithValue("@Transaction", 123456);  // = Convert.ToInt64(stockDataFromAPIModel.Result.data[i][8]);

                    sqlCommand.Parameters.AddWithValue("@StockNo", 2330);// = _stock;
                    sqlCommand.ExecuteNonQuery();


                }


            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            return;
        }


    }
}
