using AnimalWorld.Data.Models.Zoos;

namespace AnimalWorld.Core.Services.Interfaces.Zoos
{
    public interface ITicketService
    {
        Task BookZooVisit(int zooId);

        Task<List<TicketData>> GetUserTickets();
    }
}
