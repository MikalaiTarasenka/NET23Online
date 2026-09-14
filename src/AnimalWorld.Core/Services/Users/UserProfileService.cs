using AnimalWorld.Core.Services.Interfaces.Users;
using AnimalWorld.Data.Models.Users;
using AnimalWorld.Data.Repositories.Interfaces.Users;

namespace AnimalWorld.Core.Services.Users
{
    internal class UserProfileService : IUserProfileService
    {
        private IUserRepository _userRepository;
        private IAuthService _authService;

        public UserProfileService(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<UserData> Get()
        {
            var userId = _authService.GetUserId();
            var user = await _userRepository.GetById(userId);
            return user;
        }

        public async Task Update(UserData userData)
        {
            var user = await Get();
            user.FirstName = userData.FirstName;
            user.LastName = userData.LastName;
            user.PhoneNumber = userData.PhoneNumber;
            user.Language = userData.Language;
            await _userRepository.Update(user);
        }
    }
}
