using MarketPlace.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.Entities.Site
{
    public class Slider : BaseEntity
    {
        #region properties
        public string MainHeader { get; set; }
        public string SecondHeader { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Button { get; set; }
        public bool IsActive { get; set; }
        #endregion

        #region relations

        #endregion
    }
}
