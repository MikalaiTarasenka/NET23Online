namespace AnimalWorld.Web.Models.Users
{
    public class TicketViewModel
    {
        public string UniqueKey { get; set; }

        public string ZooName { get; set; }

        public DateTime EventDate { get; set; }

        public bool IsUsed { get; set; }
    }
}
