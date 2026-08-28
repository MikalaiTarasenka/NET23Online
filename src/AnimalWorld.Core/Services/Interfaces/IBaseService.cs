using AnimalWorld.Core.Dtos.Users;
using AnimalWorld.Data.Models.Common;

namespace AnimalWorld.Core.Services.Interfaces
{
    public interface IBaseService<DataModel> where DataModel : BaseModel
    {
        DataModel Get(int id);

        List<DataModel> GetAll();

        ResponseDto Create(DataModel model);

        void Update(DataModel model);

        void Delete(int id);
    }
}
