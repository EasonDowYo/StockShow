// See https://aka.ms/new-console-template for more information
using System;
using System.Text.Json;
using UpdateStockDataConsole.SQLModel;
using UpdateStockDataConsole;
using System.Data;
using UpdateStockDataConsole.TableModel;

Console.WriteLine("Hello, World!");
//string url = $@"http://www.twse.com.tw/exchangeReport/STOCK_DAY?response=json&date=20160501&stockNo=2330";

var temp_PriceDiff = Convert.ToDouble("-7.00").ToString();


DateTime dt = StockDataTableModelFunc.GetTheLastDateFromStock("2330");

DateTime dt2 = new DateTime(2023, 08, 20, 6, 20, 40);
DateTime dt22 = new DateTime(2020, 1, 1, 0, 0, 0);
StockDataTableModelFunc.UpdateStockDataToDate("2330", dt22, dt2);

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
    
