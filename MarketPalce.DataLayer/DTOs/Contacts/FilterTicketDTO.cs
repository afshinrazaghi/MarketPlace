using MarketPlace.DataLayer.DTOs.Paging;
using MarketPlace.DataLayer.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.DTOs.Contacts
{
    public class FilterTicketDTO : BasePaging
    {
        #region properties
        public string? Title { get; set; }
        public long? UserId { get; set; }
        public FilterTicketState FilterTicketState { get; set; }
        public FilterTicketOrder FilterTicketOrder { get; set; }
        public TicketSection? TicketSection { get; set; }
        public TicketPriority? TicketPriority { get; set; }

        public List<Ticket> Tickets { get; set; }
        #endregion

        #region methods
        public FilterTicketDTO SetTickets(List<Ticket> tickets)
        {
            Tickets = tickets;
            return this;
        }

        public FilterTicketDTO SetPaging(BasePaging paging)
        {
            CurrentPage = paging.CurrentPage;
            Total = paging.Total;
            PageCount = paging.PageCount;
            Skip = paging.Skip;
            Take = paging.Take;
            HowManyBeforeAndAfter = paging.HowManyBeforeAndAfter;
            First = paging.First;
            Last = paging.Last;
            return this;
        }
        #endregion
    }

    public enum FilterTicketState
    {
        All,
        NotDeleted,
        Deleted
    }

    public enum FilterTicketOrder
    {
        CreateDate_DESC,
        CreateDate_ASC
    }
}
