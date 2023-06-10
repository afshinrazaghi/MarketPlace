using MarketPlace.DataLayer.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.DTOs.Contacts
{
    public class FilterTicketDTO
    {
        public string? Title { get; set; }
        public long? UserId { get; set; }
        public FilterTicketState FilterTicketState { get; set; }
        public FilterTicketOrder FilterTicketOrder { get; set; }
        public TicketSection? TicketSection { get; set; }
        public TicketPriority? TicketPriority { get; set; }
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
