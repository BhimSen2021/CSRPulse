using CSRPulse.Model;
using CSRPulse.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Components
{
    public class MenuViewComponent : ViewComponent
    {
        private IMenuService _menuService;

        public MenuViewComponent(IMenuService menuService)
        {
            _menuService = menuService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var uDetail = new UserDetail();

            if (HttpContext.Session.GetComplexData<UserDetail>("User") != null)
            {
                uDetail = HttpContext.Session.GetComplexData<UserDetail>("User");
            }
            var uMenu = await _menuService.GetMenuByUserAsync(uDetail.RoleId);
                        
            return View("Menu",uMenu);
        }
    }
}
