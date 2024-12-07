// See https://aka.ms/new-console-template for more information
using System;
using System.Text.Json;
using UpdateStockDataConsole.SQLModel;
using UpdateStockDataConsole;
using System.Data;
using UpdateStockDataConsole.TableModel;

Console.WriteLine("Hello, World!");
try
{
    DataTable StockNoList = StockNoTableModelFunc.GetStockNoList();
    //string url = $@"http://www.twse.com.tw/exchangeReport/STOCK_DAY?response=json&date=20160501&stockNo=2330";
    if (StockNoList.Rows.Count <= 0)
    {
        Console.WriteLine("Query Fail [StockNolist]");
        return;
    }
    DateTime n = DateTime.Now;
    DateTime EndTime = new DateTime(n.Year, n.Month, n.Day, 00, 00, 00);
    DateTime StartDate = new DateTime(2020, 1, 1, 0, 0, 0);

    foreach (DataRow row in StockNoList.AsEnumerable())
    {
        //#pragma warning disable CS8600 // 正在將 Null 常值或可能的 Null 值轉換為不可為 Null 的型別。
        string StockNo = row.Field<string>("StockNo");
        //#pragma warning restore CS8600 // 正在將 Null 常值或可能的 Null 值轉換為不可為 Null 的型別。

         StockDataTableModelFunc.UpdateStockDataToDate(StockNo, StartDate, EndTime);

    }
}
catch(Exception e)
{
    Console.WriteLine(e.ToString());
}




//Task<StockDataFromAPIModel?> stockDataFromAPIModel = StockAPIFunction.GetStockDataFromAPI("2330", dt2);
//client.Timeout = TimeSpan.FromSeconds(30);
//HttpResponseMessage response = await client.GetAsync(url);
//response.EnsureSuccessStatusCode();
//string responseBody = await response.Content.ReadAsStringAsync();

//Console.WriteLine(responseBody);

//StockDataFromAPIModel?  stockDataFromAPIModel =
//JsonSerializer.Deserialize<StockDataFromAPIModel>(responseBody);
//Console.WriteLine($"Date: {stockDataFromAPIModel.date}");
//Console.WriteLine($"TemperatureCelsius: {stockDataFromAPIModel.title}");
//for(int i = 0; i < stockDataFromAPIModel.data.Count; i++)
//{
//    Console.WriteLine(stockDataFromAPIModel.data[i][0].ToString());//Date
//}
//Console.WriteLine($"Summary: {stockDataFromAPIModel?.Summary}");


//SqlDBConnect sqlDBConnect = new SqlDBConnect();

//sqlDBConnect.SelectDB = DatabaseServer.DatabaseList.StockDB;

//string sqlstring = "SELECT * FROM [Stock_db].[dbo].[StockTypeTable]";
//DataTable dataTable = sqlDBConnect.DoQuery(sqlstring);

//foreach (DataRow dr in dataTable.Rows)
//{
//    Console.WriteLine(dr["StockType"].ToString());
//}

//sqlstring = "INSERT INTO [Stock_db].[dbo].[StockTypeTable] (StockType) VALUES ( 'TEST'); ";
//sqlDBConnect.DoExecute(sqlstring);



Console.WriteLine("[End]");
    
