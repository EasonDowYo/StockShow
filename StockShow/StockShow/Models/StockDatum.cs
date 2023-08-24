using System;
using System.Collections.Generic;

namespace StockShow.Models
{
    public partial class StockDatum
    {
        public DateTime Date { get; set; }
        public double DealStocks { get; set; }
        public double DealPrice { get; set; }
        public string OpeningPrice { get; set; } = null!;
        public string HighPriceInDay { get; set; } = null!;
        public string LowPriceInDay { get; set; } = null!;
        public string ClosingPrice { get; set; } = null!;
        public string PriceDiff { get; set; } = null!;
        public string DealNumber { get; set; } = null!;
        public string StockNo { get; set; } = null!;
    }
}
