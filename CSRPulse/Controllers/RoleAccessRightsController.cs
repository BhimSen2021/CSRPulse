using CSRPulse.Model;
using CSRPulse.Models;
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
        private readonly IRoleAccessRightsServices _accessRightsServices;

        public RoleAccessRightsController(IDropdownBindService dropdownBindService, IRoleAccessRightsServices accessRightsServices)
        {
            _ddlService = dropdownBindService;
            _accessRightsServices = accessRightsServices;
        }
        public IActionResult Index()
        {
            BindDropdowns();
            return View();
        }

        public async Task<ActionResult> GetRoleAccessRights(UserRight right)
        {
            _logger.LogInformation("RoleAccessRightsController/GetRoleAccessRights");
            try
            {
                RoleAccessViewModel roleAccessVM = new RoleAccessViewModel();
                roleAccessVM.menuParentList = await _accessRightsServices.GetMenuListAsync();
                roleAccessVM.menuChildList = await _accessRightsServices.GetRoleAccessMenuListAsync(right.RoleId);
                return PartialView("_RoleAccessRights", roleAccessVM);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw ex;
            }

        }

        [NonAction]
        void BindDropdowns()
        {
            var roleList = _ddlService.GetRole(null);
            ViewBag.ddlRole = new SelectList(roleList, "id", "value");
        }
    }
}
