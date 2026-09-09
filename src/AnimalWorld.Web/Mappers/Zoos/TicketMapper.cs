using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Models.Users;

namespace AnimalWorld.Web.Mappers.Zoos
{
    public class TicketMapper : IMapper<TicketData, TicketViewModel>
    {
        public TicketViewModel Map(TicketData source)
        {
            return new TicketViewModel
            {
                UniqueKey = source.UniqueKey,
                ZooName = source.Zoo.Name,
                EventDate = source.EventDate,
                IsUsed = source.IsUsed
            };
        }
    }
}
