using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Contacts;
using MarketPlace.Web.PresentationExtensions;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.User.Controllers
{
    public class TicketController : UserBaseController
    {
        #region constuctor
        private readonly IContactService _contactService;
        public TicketController(IContactService contactService)
        {
            _contactService = contactService;
        }
        #endregion

        #region list
        [HttpGet("tickets")]
        public async Task<IActionResult> Index(FilterTicketDTO filter)
        {
            filter.UserId = User.GetUserId();
            filter.FilterTicketState = FilterTicketState.NotDeleted;
            filter.FilterTicketOrder = FilterTicketOrder.CreateDate_DESC;
            var data = await _contactService.FilterTicket(filter);
            return View(data);
        }
        #endregion

        #region create-ticket
        [HttpGet("create-ticket")]
        public IActionResult CreateTicket()
        {
            return View();
        }

        [HttpPost("create-ticket")]
        public async Task<IActionResult> CreateTicket(CreateTicketDTO model)
        {
            if (ModelState.IsValid)
            {
                var res = await _contactService.CreateUserTicket(model, User.GetUserId()!.Value);
                switch (res)
                {
                    case CreateTicketResult.Error:
                        TempData[ErrorMessage] = "در انجام عملیات خطایی رخ داده است";
                        break;
                    case CreateTicketResult.Success:
                        TempData[SuccessMessage] = "تیکت شما با موفقیت ثبت شد";
                        TempData[InfoMessage] = "پاسخ شما به زودی ارسال می گردد";
                        return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        #endregion

        #region ticket-detail
        [HttpGet("tickets/{ticketId}")]
        public async Task<IActionResult> TicketDetail(long ticketId)
        {
            var ticket = await _contactService.GetTicketForShow(ticketId, User.GetUserId()!.Value);
            return View(ticket);
        }

        #endregion
    }
}
