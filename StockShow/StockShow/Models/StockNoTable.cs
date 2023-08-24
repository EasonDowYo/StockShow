using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations; //For Validation Attribute 
using System.Diagnostics.CodeAnalysis;

namespace StockShow.Models
{
    public partial class StockNoTable
    {
        [Required]
        [StringLength(10)]
        public string StockNo { get; set; } = null!;
        public string StockName { get; set; } = null!;

        //原先會自動卡空值
        [AllowNull]
        public string? StockNoName { get; set; }
        public string StockType { get; set; } = null!;
        public int StockTypeIndex { get; set; }
        public string? Note1 { get; set; }
        public string? Note2 { get; set; }
        public string? Note3 { get; set; }
        public string? Note4 { get; set; }
        public string? Note5 { get; set; }
        public string? Note6 { get; set; }
        public string? Note7 { get; set; }

        public virtual StockTypeTable? StockTypeIndexNavigation { get; set; } 
    }
}
