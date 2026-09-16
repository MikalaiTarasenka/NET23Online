using Microsoft.AspNetCore.Authentication.Cookies;

namespace AnimalWorld.Core.Settings
{
    public static class AuthConstants
    {
        public const string AUTH_KEY = CookieAuthenticationDefaults.AuthenticationScheme;
        public const string COOKIE_ID_KEY = "Id";
        public const string COOKIE_ROLE_KEY = "Role";
        public const string COOKIE_NAME_KEY = "UserName";
        public const string COOKIE_LANGUAGE_KEY = "Language";
    }
}
