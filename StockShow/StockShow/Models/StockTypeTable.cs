using System;
using System.Collections.Generic;

namespace StockShow.Models
{
    public partial class StockTypeTable
    {
        public StockTypeTable()
        {
            //StockNoTables = new HashSet<StockNoTable>();
        }

        public int StockTypeIndex { get; set; }
        public string StockType { get; set; } = null!;

        //public virtual ICollection<StockNoTable> StockNoTables { get; set; }
    }
}
