using MarketPlace.DataLayer.Entities.Account;
using MarketPlace.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.Entities.Contacts
{
    public class Ticket:BaseEntity
    {
        #region constcructor
        public Ticket()
        {
            TickerMessages = new HashSet<TickerMessage>();
        }
        #endregion

        #region properties
        public long OwnerId { get; set; }
        public string Title { get; set; }
        public TicketSection TicketSection { get; set; }

        public TicketPriority TicketPriority { get; set; }
        public TicketStatus TicketStatus { get; set; }
        #endregion

        #region relations
        public virtual User Owner { get; set; }
        public virtual ICollection<TickerMessage> TickerMessages { get; set; }

        #endregion
    }

    public enum TicketSection
    {
        [Display(Name ="پشتیبانی")]
        Support,
        [Display(Name = "فنی")]
        Technical,
        [Display(Name = "آموزشی")]
        Academic
    }

    public enum TicketPriority
    {
        [Display(Name = "کم")]
        Low,
        [Display(Name = "متوسط")]
        Medium,
        [Display(Name = "زیاد")]
        Hight
    }

    public enum TicketStatus
    {
        [Display(Name = "در حال بررسی")]
        UnderProgress,
        [Display(Name = "پاسخ داده شده")]
        Answered,
        [Display(Name = "بسته شده")]
        Closed

    }
}
