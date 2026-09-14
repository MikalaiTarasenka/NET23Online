using AnimalWorld.Core.Dtos.Users;
using AnimalWorld.Core.Services.Interfaces.Users;
using AnimalWorld.Core.Settings;
using AnimalWorld.Data.Enums;
using AnimalWorld.Data.Models.Users;
using AnimalWorld.Data.Repositories.Interfaces.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AnimalWorld.Core.Services.Users
{
    internal class AuthService : IAuthService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public AuthService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public int GetUserId()
        {
            var userIdStr = _httpContextAccessor.HttpContext!.User?.Claims.FirstOrDefault(x => x.Type == AuthConstants.COOCKIE_ID_KEY)?.Value;
            if (userIdStr is null)
            {
                return 0;
            }

            var userId = int.Parse(userIdStr);
            return userId;
        }

        public string GetUserName()
        {
            var userName = _httpContextAccessor.HttpContext!.User?.Claims.FirstOrDefault(x => x.Type == AuthConstants.COOCKIE_NAME_KEY)?.Value;
            return userName;
        }

        public async Task<UserData> GetUser()
        {
            var userId = GetUserId();
            if (userId <= 0)
            {
                return null;
            }

            var user = await _userRepository.GetById(userId);
            return user;
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

            var roleStr = _httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == AuthConstants.COOCKIE_ROLE_KEY).Value;
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

            var languageStr = _httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == AuthConstants.COOCKIE_LANGUAGE_KEY).Value;
            var language = Enum.Parse<Language>(languageStr);
            return language;
        }

        public async Task<ResponseDto> Login(CredentialsDto credentialsDto)
        {
            var user = await _userRepository.GetUser(credentialsDto.UserName);
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

            await SignIn(user);
            return new ResponseDto
            {
                Success = true
            };
        }

        public async Task<ResponseDto> Register(CredentialsDto credentialsDto)
        {
            if (!await _userRepository.UserNameIsFree(credentialsDto.UserName))
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
            await _userRepository.Create(user);
            return new ResponseDto
            {
                Success = true
            };
        }

        public async Task SignIn(UserData user)
        {
            var claims = new List<Claim>
            {
                new Claim(AuthConstants.COOCKIE_ID_KEY, user.Id.ToString()),
                new Claim(AuthConstants.COOCKIE_ROLE_KEY, user.Role.ToString()),
                new Claim(AuthConstants.COOCKIE_NAME_KEY, user.UserName),
                new Claim(AuthConstants.COOCKIE_LANGUAGE_KEY, user.Language.ToString()),
                new Claim(ClaimTypes.AuthenticationMethod, AuthConstants.AUTH_KEY)
            };
            var identity = new ClaimsIdentity(claims, AuthConstants.AUTH_KEY);
            var principal = new ClaimsPrincipal(identity);
            await _httpContextAccessor.HttpContext!.SignInAsync(AuthConstants.AUTH_KEY, principal);
        }
    }
}
