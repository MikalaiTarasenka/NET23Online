using AnimalWorld.Data.Models.Zoos;

namespace AnimalWorld.Core.Services.Interfaces.Zoos
{
    public interface ITicketService
    {
        void BookZooVisit(string zooName);

        List<TicketData> GetUserTickets(int userId);
    }
}
