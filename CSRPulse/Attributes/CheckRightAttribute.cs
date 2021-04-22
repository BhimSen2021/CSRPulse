using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Linq;
using CSRPulse.Model;
using CSRPulse.Common;
namespace CSRPulse.Attributes
{
    public class CheckRightAttribute : ActionFilterAttribute, IActionFilter
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string actionName = string.Empty;
            string controllerName = string.Empty;
            // string actionName = filterContext.ActionDescriptor.ActionConstraints.ToString();
            var controllerActionDescriptor= filterContext.ActionDescriptor as ControllerActionDescriptor;
      
            if (controllerActionDescriptor != null)
            {
                controllerName = controllerActionDescriptor.ControllerName.ToString().ToLower();
                actionName = controllerActionDescriptor.ActionName.ToString();
            }
              var user = filterContext.HttpContext.Session.GetComplexData<UserDetail>("user");

            var indexPageURL = $"{controllerName}/index";

            if (!SharedControllers.controllers.Contains(controllerName))
            {

                if (user != null && actionName != "index" && user.userMenuRights.Count > 0)
                {
                    var menuRights = user.userMenuRights;
                    if (menuRights.Any(x => x.menu.URL.ToLower() == indexPageURL))
                    {
                        var menuRight = menuRights.Where(x => x.menu.URL.ToLower() == indexPageURL).Select(
                            y => new { ShowMenuRight = y.ShowMenu, CreateRight = y.Create, EditRight = y.Edit }).FirstOrDefault();

                        if (actionName == "index" && !menuRight.ShowMenuRight)
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "UnAuthorizeAccess", controller = "MenuRender" }));

                        if (actionName == "edit" && !menuRight.EditRight)
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "UnAuthorizeAccess", controller = "MenuRender" }));

                        if (actionName == "create" && !menuRight.CreateRight)
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "UnAuthorizeAccess", controller = "MenuRender" }));
                    }
                    else
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "UnAuthorizeAccess", controller = "MenuRender" }));
                }
                else
                {
                    if (user != null)

                    {
                        if (user.userMenuRights.Count > 0)
                        {
                            if (!user.userMenuRights.Any(x => x.menu.URL.ToLower() == indexPageURL))
                                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "UnAuthorizeAccess", controller = "MenuRender" }));
                        }
                        else
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "UnAuthorizeAccess", controller = "MenuRender" }));

                        base.OnActionExecuting(filterContext);
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Login" }));
                        base.OnActionExecuting(filterContext);

                    }
                }
            }

        }
    }
}
