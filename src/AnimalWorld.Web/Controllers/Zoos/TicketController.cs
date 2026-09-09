using AnimalWorld.Core.Services.Interfaces.Zoos;
using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Web.Attributes;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalWorld.Web.Controllers.Zoos
{
    [Authorize]
    public class TicketController : Controller
    {
        private ITicketService _ticketService;
        private IMapper<TicketData, TicketViewModel> _mapper;

        public TicketController(ITicketService ticketService, IMapper<TicketData, TicketViewModel> mapper)
        {
            _ticketService = ticketService;
            _mapper = mapper;
        }

        [Booking]
        public IActionResult Book(int zooId)
        {
            _ticketService.BookZooVisit(zooId);
            return View();
        }

        public IActionResult BookingDeny()
        {
            return View();
        }

        public IActionResult Tickets()
        {
            var ticketDatas = _ticketService.GetUserTickets();
            var tickets = _mapper.MapList(ticketDatas);
            return View(tickets);
        }
    }
}
