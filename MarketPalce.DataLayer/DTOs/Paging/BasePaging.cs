using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.DTOs.Paging
{
    public class BasePaging
    {
        public BasePaging()
        {
            CurrentPage = 1;
            Take = 10;
            HowManyBeforeAndAfter = 3;
        }


        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int Total { get; set; }
        public int First { get; set; }
        public int Last { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public int HowManyBeforeAndAfter { get; set; }

        public BasePaging GetCurrentPaging()
        {
            return this;
        }
    }
}
