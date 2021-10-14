using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSRPulse.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CSRPulse.Controllers
{
    public class RoleRightsController : BaseController<RoleRightsController>
    {
        private readonly IDropdownBindService _ddlService;
        public RoleRightsController(IDropdownBindService dropdownBindService)
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
