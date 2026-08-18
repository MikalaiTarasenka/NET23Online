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

        void SignIn(UserData user);
    }
}
