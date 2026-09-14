using AnimalWorld.Core.Services.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AnimalWorld.Web.Attributes
{
    public class AtLeastModeratorAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authService = context
                .HttpContext
                .RequestServices
                .GetRequiredService<IAuthService>();
            if (!authService.IsAtLeastModerator())
            {
                context.Result = ((Controller)context.Controller).RedirectToAction("AccessDenied", "Auth");
                return;
            }

            await next();
        }
    }
}
