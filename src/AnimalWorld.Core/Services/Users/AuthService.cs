using AnimalWorld.Core.Dtos.Users;
using AnimalWorld.Core.Services.Interfaces.Users;
using AnimalWorld.Data.Enums;
using AnimalWorld.Data.Models.Users;
using AnimalWorld.Data.Repositories.Interfaces.Users;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using System.Security.Claims;

namespace AnimalWorld.Core.Services.Users
{
    internal class AuthService : IAuthService
    {
        public const string AUTH_KEY = "QGHyrFBGxnQR";
        public const string COOCKIE_ID_KEY = "Id";
        public const string COOCKIE_ROLE_KEY = "Role";
        public const string COOCKIE_NAME_KEY = "UserName";
        public const string COOCKIE_LANGUAGE_KEY = "Language";
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public AuthService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public int GetUserId()
        {
            var userIdStr = _httpContextAccessor.HttpContext!.User?.Claims.FirstOrDefault(x => x.Type == COOCKIE_ID_KEY)?.Value;
            if (userIdStr is null)
            {
                return 0;
            }

            var userId = int.Parse(userIdStr);
            return userId;
        }

        public string GetUserName()
        {
            var userName = _httpContextAccessor.HttpContext!.User?.Claims.FirstOrDefault(x => x.Type == COOCKIE_NAME_KEY)?.Value;
            return userName;
        }

        public UserData GetUser()
        {
            var userId = GetUserId();
            if (userId <= 0)
            {
                return null;
            }

            return _userRepository.GetById(userId);
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }

        public UserRole GetRole()
        {
            if (!IsAuthenticated())
            {
                throw new InvalidOperationException();
            }

            var roleStr = _httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == COOCKIE_ROLE_KEY).Value;
            var role = Enum.Parse<UserRole>(roleStr);
            return role;
        }

        public bool IsAtLeastModerator()
        {
            if (!IsAuthenticated())
            {
                return false;
            }

            var role = GetRole();
            return role == UserRole.Moderator || role == UserRole.Admin;
        }

        public Language GetLanguage()
        {
            if (!IsAuthenticated())
            {
                return Language.English;
            }

            var languageStr = _httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == COOCKIE_LANGUAGE_KEY).Value;
            var language = Enum.Parse<Language>(languageStr);
            return language;
        }

        public ResponseDto Login(CredentialsDto credentialsDto)
        {
            var user = _userRepository.GetUser(credentialsDto.UserName);
            if (user == null)
            {
                return new ResponseDto
                {
                    Success = false,
                    Error = "Неверные имя пользователя и/или пароль"
                };
            }

            if (!BCrypt.Net.BCrypt.Verify(credentialsDto.Password, user.PasswordHash))
            {
                return new ResponseDto
                {
                    Success = false,
                    Error = "Неверные имя пользователя и/или пароль"
                };
            }

            SignIn(user);
            return new ResponseDto
            {
                Success = true
            };
        }

        public ResponseDto Register(CredentialsDto credentialsDto)
        {
            if (!_userRepository.UserNameIsFree(credentialsDto.UserName))
            {
                return new ResponseDto
                {
                    Success = false,
                    Error = "Имя пользователя занято"
                };
            }

            var user = new UserData
            {
                UserName = credentialsDto.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(credentialsDto.Password),
                Role = UserRole.User,
                Language = Language.English
            };
            _userRepository.Create(user);
            return new ResponseDto
            {
                Success = true
            };
        }

        public void SignIn(UserData user)
        {
            var claims = new List<Claim>
            {
                new Claim(COOCKIE_ID_KEY, user.Id.ToString()),
                new Claim(COOCKIE_ROLE_KEY, user.Role.ToString()),
                new Claim(COOCKIE_NAME_KEY, user.UserName),
                new Claim(COOCKIE_LANGUAGE_KEY, user.Language.ToString()),
                new Claim(ClaimTypes.AuthenticationMethod, AUTH_KEY)
            };
            var identity = new ClaimsIdentity(claims, AUTH_KEY);
            var principal = new ClaimsPrincipal(identity);
            _httpContextAccessor.HttpContext!
                .SignInAsync(AUTH_KEY, principal)
                .Wait();
        }
    }
}
