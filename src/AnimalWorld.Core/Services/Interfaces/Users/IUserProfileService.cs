using AnimalWorld.Data.Models.Users;

namespace AnimalWorld.Core.Services.Interfaces.Users
{
    public interface IUserProfileService
    {
        Task<UserData> Get();

        Task Update(UserData userData);
    }
}
