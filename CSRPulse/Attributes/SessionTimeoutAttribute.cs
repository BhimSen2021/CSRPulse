using CSRPulse.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace CSRPulse.Attributes
{
    public class SessionTimeoutAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userDetail = filterContext.HttpContext.Session.GetComplexData<UserDetail>("User");


            if (userDetail == null)
            {
                if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    filterContext.HttpContext.Response.StatusCode = StatusCodes.Status308PermanentRedirect; base.OnActionExecuting(filterContext);
                }

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Login", controller = "Account", Area = "Admin" }));
                base.OnActionExecuting(filterContext);

            }
        }
    }
}
