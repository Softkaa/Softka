using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Softka.Utils
{
    public class AuthorizationRequiredAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if(!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new {
                    controller = "Login", action = "Index"
                }));
            }
        }
    }
}