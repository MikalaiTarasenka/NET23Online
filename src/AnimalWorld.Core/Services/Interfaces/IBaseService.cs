using AnimalWorld.Data.Models.Common;

namespace AnimalWorld.Core.Services.Interfaces
{
    public interface IBaseService<DataModel> where DataModel : BaseModel
    {
        void Create(DataModel model);
    }
}
