using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Data.Repositories.Common;
using AnimalWorld.Data.Repositories.Interfaces.Zoos;
using Microsoft.EntityFrameworkCore;

namespace AnimalWorld.Data.Repositories.Zoos
{
    internal class TicketRepository : BaseRepository<TicketData>, ITicketRepository
    {
        public TicketRepository(WebContext context) : base(context) { }

        public List<TicketData> GetUserTickets(int userId)
        {
            return _dbSet
                .Where(x => x.UserId == userId)
                .Include(x => x.Zoo)
                .ToList();
        }
    }
}
