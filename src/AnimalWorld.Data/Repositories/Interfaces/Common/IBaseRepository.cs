using AnimalWorld.Data.Models.Common;

namespace AnimalWorld.Data.Repositories.Interfaces.Common
{
    public interface IBaseRepository<DataModel> where DataModel : BaseModel
    {
        Task<List<DataModel>> GetAll();

        Task<DataModel> GetById(int id);

        Task Create(DataModel model);

        Task Update(DataModel model);

        Task Delete(int id);
    }
}
