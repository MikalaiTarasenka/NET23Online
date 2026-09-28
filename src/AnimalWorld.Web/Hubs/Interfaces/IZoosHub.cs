namespace AnimalWorld.Web.Hubs.Interfaces
{
    public interface IZoosHub
    {
        Task AnimalAppearedMessage(string message);
    }
}
