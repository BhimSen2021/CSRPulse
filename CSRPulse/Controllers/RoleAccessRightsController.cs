using CSRPulse.Model;
using CSRPulse.Services;
using CSRPulse.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Controllers
{
    public class RoleAccessRightsController : BaseController<RoleAccessRightsController>
    {
        private readonly IDropdownBindService _ddlService;
        public RoleAccessRightsController(IDropdownBindService dropdownBindService)
        {
            _ddlService = dropdownBindService;
        }
        public IActionResult Index()
        {
            BindDropdowns();
            return View();
        }

        [NonAction]
        void BindDropdowns()
        {
            var roleList = _ddlService.GetRole(null);
            ViewBag.ddlRole = new SelectList(roleList, "id", "value");
        }
    }
}
