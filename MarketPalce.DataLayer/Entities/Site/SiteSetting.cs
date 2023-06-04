using MarketPlace.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.Entities.Site
{
    public class SiteSetting : BaseEntity
    {
        #region properties
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? FooterText { get; set; }
        public string? CopyRight { get; set; }
        public string? Address { get; set; }
        public bool IsDefault { get; set; }
        #endregion

        #region relations

        #endregion
    }
}
