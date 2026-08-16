using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Data.Repositories.Interfaces.Common;

namespace AnimalWorld.Data.Repositories.Interfaces.Zoos
{
    public interface ITicketRepository : IBaseRepository<TicketData>
    {
        List<TicketData> GetUserTickets(int userId);
    }
}
