using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateStockDataConsole
{

    public class StockDataFromAPIModel
    {
        public string stat { get; set; } = string.Empty;
        public string date { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
        public List<string>? fields { get; set; }
        public List<List<string>>? data { get; set; }
        public List<string>? notes { get; set; } 
    }
}
