using AnimalWorld.Core.Services.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AnimalWorld.Web.Attributes
{
    public class BookingAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authService = context
                .HttpContext
                .RequestServices
                .GetRequiredService<IAuthService>();
            var user = await authService.GetUser();
            if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName) || string.IsNullOrEmpty(user.PhoneNumber))
            {
                context.Result = ((Controller)context.Controller).RedirectToAction("BookingDeny", "Ticket");
                return;
            }

            await next();
        }
    }
}
