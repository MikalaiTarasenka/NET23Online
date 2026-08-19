using AnimalWorld.Core.Services.Interfaces.Users;
using AnimalWorld.Core.Services.Interfaces.Zoos;
using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Data.Repositories.Interfaces.Zoos;

namespace AnimalWorld.Core.Services.Zoos
{
    internal class TicketService : ITicketService
    {
        private IAuthService _authService;
        private ITicketRepository _ticketRepository;
        private IZooRepository _zooRepository;

        public TicketService(IAuthService authService, ITicketRepository ticketRepository, IZooRepository zooRepository)
        {
            _authService = authService;
            _ticketRepository = ticketRepository;
            _zooRepository = zooRepository;
        }

        public void BookZooVisit(string zooName)
        {
            var user = _authService.GetUser();
            var zoo = _zooRepository.GetByName(zooName);
            TicketData ticketData = new TicketData
            {
                User = user,
                UserId = user.Id,
                Zoo = zoo,
                ZooId = zoo.Id,
                EventDate = DateTime.UtcNow.AddMonths(1),
                UniqueKey = Guid.NewGuid().ToString()
            };

            _ticketRepository.Create(ticketData);
        }

        public List<TicketData> GetUserTickets(int userId)
        {
            var tickets = _ticketRepository.GetUserTickets(userId);
            return tickets;
        }
    }
}
