using AnimalWorld.Core.Dtos.Users;
using AnimalWorld.Data.Enums;
using AnimalWorld.Data.Models.Users;

namespace AnimalWorld.Core.Services.Interfaces.Users
{
    public interface IAuthService
    {
        int GetUserId();

        string GetUserName();

        Task<UserData> GetUser();

        bool IsAuthenticated();

        UserRole GetRole();

        bool IsAtLeastModerator();

        Language GetLanguage();

        Task<ResponseDto> Login(CredentialsDto credentialsDto);

        Task<ResponseDto> Register(CredentialsDto credentialsDto);

        Task SignIn(UserData user);
    }
}
