using AnimalWorld.Data.Models.Zoos;

namespace AnimalWorld.Core.Services.Interfaces.Zoos
{
    public interface ITicketService
    {
        void BookZooVisit(int zooId);

        List<TicketData> GetUserTickets();
    }
}
