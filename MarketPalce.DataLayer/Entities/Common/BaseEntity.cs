using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.Entities.Common
{
    public class BaseEntity<TEntity>
    {
        public TEntity Id { get; set; }
    }
}
