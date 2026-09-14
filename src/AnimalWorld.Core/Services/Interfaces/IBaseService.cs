using AnimalWorld.Core.Dtos.Users;
using AnimalWorld.Data.Models.Common;

namespace AnimalWorld.Core.Services.Interfaces
{
    public interface IBaseService<DataModel> where DataModel : BaseModel
    {
        Task<DataModel> Get(int id);

        Task<List<DataModel>> GetAll();

        Task<ResponseDto> Create(DataModel model);

        Task Update(DataModel model);

        Task Delete(int id);
    }
}
