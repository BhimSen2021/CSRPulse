using CSRPulse.Model;
using CSRPulse.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Components
{
    public class UserMenuViewComponent : ViewComponent
    {
        private IMenuService _menuService;

        public UserMenuViewComponent(IMenuService menuService)
        {
            _menuService = menuService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            UserDetail uDetail = new UserDetail();

            if (HttpContext.Session.GetComplexData<UserDetail>("User") != null)
            {
                uDetail = HttpContext.Session.GetComplexData<UserDetail>("User");
            }

            var listMenu = _menuService.GetMenuByUserAsync(uDetail.UserID);
            return View(listMenu);
        }
    }
}
