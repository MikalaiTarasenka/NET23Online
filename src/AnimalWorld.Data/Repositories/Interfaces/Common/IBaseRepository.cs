using AnimalWorld.Data.Models.Common;

namespace AnimalWorld.Data.Repositories.Interfaces.Common
{
    public interface IBaseRepository<DataModel> where DataModel : BaseModel
    {
        List<DataModel> GetAll();

        DataModel GetById(int id);

        void Create(DataModel model);

        void Update(DataModel model);

        void Delete(int id);
    }
}
