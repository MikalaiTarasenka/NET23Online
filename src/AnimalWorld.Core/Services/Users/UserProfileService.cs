using AnimalWorld.Core.Services.Interfaces.Users;
using AnimalWorld.Data.Models.Users;
using AnimalWorld.Data.Repositories.Interfaces.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public UserData Get()
        {
            var userId = _authService.GetUserId();
            var user = _userRepository.GetById(userId);
            return user;
        }

        public void Update(UserData userData)
        {
            var userId = _authService.GetUserId();
            var user = _userRepository.GetById(userId);
            user.FirstName = userData.FirstName;
            user.LastName = userData.LastName;
            user.PhoneNumber = userData.PhoneNumber;
            user.Language = userData.Language;
            _userRepository.Update(user);
        }
    }
}
