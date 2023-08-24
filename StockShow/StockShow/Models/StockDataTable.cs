using System;
using System.Collections.Generic;

namespace StockShow.Models
{
    public partial class StockDataTable
    {
        public DateTime Data { get; set; }
        public long DealStocks { get; set; }
        public long DealPrice { get; set; }
        public double OpeningPrice { get; set; }
        public double ClosingPrice { get; set; }
        public double HighestPrice { get; set; }
        public double LowestPrice { get; set; }
        public double PriceDiff { get; set; }
        public long DealNumber { get; set; }
        public string? StockNo { get; set; }

        public virtual StockNoTable? StockNoNavigation { get; set; }
    }
}
