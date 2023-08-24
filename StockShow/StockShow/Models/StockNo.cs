using System;
using System.Collections.Generic;

namespace StockShow.Models
{
    public partial class StockNo
    {
        public string StockNumber { get; set; } = null!;
        public string StockName { get; set; } = null!;
        public string StockType { get; set; } = null!;
        public string? Remarks { get; set; }
        public string? Remarks2 { get; set; }
        public string? Remarks3 { get; set; }
        public int StockIndex { get; set; }
        public string StockNumberName { get; set; } = null!;
    }
}
