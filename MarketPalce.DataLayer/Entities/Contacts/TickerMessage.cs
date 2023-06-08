using MarketPlace.DataLayer.Entities.Account;
using MarketPlace.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.Entities.Contacts
{
    public class TickerMessage : BaseEntity
    {
        #region properties
        public long TicketId { get; set; }
        public long SenderId { get; set; }
        public string Text { get; set; }
        #endregion

        #region relations
        public virtual Ticket Ticket { get; set; }
        public virtual User Sender { get; set; }
        #endregion

    }
}
