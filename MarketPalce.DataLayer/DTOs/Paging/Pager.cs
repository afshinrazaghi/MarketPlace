using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.DTOs.Paging
{
    public class Pager
    {
        public static BasePaging Build(int currentPage, int total, int take, int howManyBeforeAndAfter)
        {
            int pageCount = Convert.ToInt32(Math.Ceiling(total / (double)take));

            var basePaging = new BasePaging
            {
                CurrentPage = currentPage,
                Total = total,
                Take = take,
                PageCount = pageCount,
                First = (currentPage - howManyBeforeAndAfter) >= 1 ? currentPage - howManyBeforeAndAfter : 1,
                Last = (currentPage + howManyBeforeAndAfter) <= pageCount ? currentPage - howManyBeforeAndAfter : pageCount,
                Skip = (currentPage - 1) * take,
                HowManyBeforeAndAfter = howManyBeforeAndAfter
            };

            return basePaging;
        }
    }
}
