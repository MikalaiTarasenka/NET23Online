using AnimalWorld.Data.Models.Common;

namespace AnimalWorld.Data.Repositories.Interfaces.Common
{
    public interface INamedBaseRepository<DataModel> : IBaseRepository<DataModel> where DataModel : NamedBaseModel
    {
        DataModel GetByName(string name);
    }
}
