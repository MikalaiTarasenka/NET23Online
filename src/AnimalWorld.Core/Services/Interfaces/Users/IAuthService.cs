using AnimalWorld.Core.Dtos.Users;
using AnimalWorld.Data.Enums;
using AnimalWorld.Data.Models.Users;

namespace AnimalWorld.Core.Services.Interfaces.Users
{
    public interface IAuthService
    {
        int GetUserId();

        string GetUserName();

        UserData GetUser();

        bool IsAuthenticated();

        UserRole GetRole();

        bool IsAtLeastModerator();

        Language GetLanguage();

        AuthResultDto Login(CredentialsDto credentialsDto);

        AuthResultDto Register(CredentialsDto credentialsDto);

        void SignIn(UserData user);
    }
}
