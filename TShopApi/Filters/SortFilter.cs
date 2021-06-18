using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TShopApi.Filters
{
    public class SortFilter
    {
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }

        public SortFilter() {
            this.SortColumn = null;
            this.SortOrder = "asc";
        }

        public SortFilter(string sortColumn, string sortOrder)
        {
            this.SortColumn = sortColumn;
            this.SortOrder = sortOrder;
        }
    }
}
