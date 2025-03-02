﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Linq;
using CSRPulse.Model;
using CSRPulse.Common;

namespace CSRPulse.Attributes
{
    public class CheckAuthorizeAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
            string actionName = string.Empty;
            string controllerName = string.Empty;
           
            var controllerActionDescriptor = filterContext.ActionDescriptor as ControllerActionDescriptor;

            if (controllerActionDescriptor != null)
            {
                controllerName = controllerActionDescriptor.ControllerName.ToString().ToLower();
                actionName = controllerActionDescriptor.ActionName.ToString().ToLower();
            }
            var user = filterContext.HttpContext.Session.GetComplexData<UserDetail>("User");
           
            var indexPageURL = $"{controllerName}/index";


            if (!SharedControllers.controllers.Contains(controllerName))
            {
                if (user != null && actionName != "index" && user.userMenuRights.Count > 0)
                {
                    var menuRights = user.userMenuRights;

                    if (menuRights.Any(x => x.menu.Url == (indexPageURL)))
                    {
                        var menuRight = menuRights.Where(x => x.menu.Url == indexPageURL).Select(
                            y => new { ShowMenuRight = y.ShowMenu, CreateRight = y.CreateRight, EditRight = y.EditRight, ViewRight = y.ViewRight, DeleteRight = y.DeleteRight }).FirstOrDefault();

                        if (actionName == "index" && !menuRight.ShowMenuRight)
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "UnAuthorizeAccess", controller = "Handler" }));

                        if (actionName == "edit" && !menuRight.EditRight)
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "UnAuthorizeAccess", controller = "Handler" }));

                        if (actionName == "create" && !menuRight.CreateRight)
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "UnAuthorizeAccess", controller = "Handler" }));

                        if (actionName == "details" && !menuRight.ViewRight)
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "UnAuthorizeAccess", controller = "Handler" }));

                        if (actionName == "delete" && !menuRight.DeleteRight)
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "UnAuthorizeAccess", controller = "Handler" }));
                    }
                    else
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "UnAuthorizeAccess", controller = "Handler" }));
                }
                else
                {
                    if (user != null)
                    {
                        if (user.userMenuRights.Count > 0)
                        {
                            if (!user.userMenuRights.Any(x => x.menu.Url == indexPageURL))
                                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "UnAuthorizeAccess", controller = "Handler" }));
                        }
                        else
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "UnAuthorizeAccess", controller = "Handler" }));

                        base.OnActionExecuting(filterContext);
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Logout", controller = "Account",Area="Admin" }));
                        base.OnActionExecuting(filterContext);

                    }
                }
            }

        }
    }
}
