using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UpdateStockDataConsole
{
    internal class StockAPIFunction
    {


        public static async Task<StockDataFromAPIModel?> GetStockDataFromAPI(string _stockNo, DateTime _date)
        {
            Console.WriteLine($@"[Calling Stock API] GetStockDataFromAPI  {_stockNo} {_date.ToString("yyyy-MM-dd")}" );
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    
                    string dateString=_date.ToString("yyyyMMdd");
                    string url = $@"http://www.twse.com.tw/exchangeReport/STOCK_DAY?response=json&date={dateString}&stockNo={_stockNo}";

                    client.Timeout = TimeSpan.FromSeconds(30);

                    HttpResponseMessage response = await client.GetAsync(url);

                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    StockDataFromAPIModel? stockDataFromAPIModel = JsonSerializer.Deserialize<StockDataFromAPIModel>(responseBody);
                    return stockDataFromAPIModel;
                }
                catch(Exception ex)
                {
                    Console.WriteLine("[Calling Stock API ERROR]"+ex.ToString());
                    return null;
                }
            }
        }
    }
}
