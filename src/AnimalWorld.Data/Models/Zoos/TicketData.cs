using AnimalWorld.Data.Models.Common;
using AnimalWorld.Data.Models.Users;

namespace AnimalWorld.Data.Models.Zoos
{
    public class TicketData : BaseModel
    {
        public string UniqueKey { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

        public DateTime EventDate { get; set; }

        public bool IsUsed { get; set; } = false;

        public int UserId { get; set; }

        public int ZooId { get; set; }

        public UserData User { get; set; }

        public ZooData Zoo { get; set; }
    }
}
