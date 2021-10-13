using CSRPulse.Model;
using CSRPulse.Models;
using CSRPulse.Services;
using CSRPulse.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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


        public ActionResult InsertUpdateRoleAccessRights(int roleId, string jsonInput)
        {
            try
            {

                var arrayRoleAccessRight = JsonConvert.DeserializeObject<string[]>(jsonInput);
                List<RoleAccessRights> listUpdateRoleAccessRights = new List<RoleAccessRights>();
                BaseModel baseModel = new BaseModel()
                {
                    CreatedBy = userDetail.UserID,
                    CreatedOn = DateTime.Now,
                    CreatedRid = userDetail.RoleId,
                    CreatedRname = userDetail.RoleName,
                    UpdatedBy = userDetail.UserID,
                    UpdatedOn = DateTime.Now,
                    UpdatedRid = userDetail.RoleId,
                    UpdatedRname = userDetail.RoleName,
                };

                if (arrayRoleAccessRight != null)
                {
                    for (int i = 0; i < arrayRoleAccessRight.Length; i++)
                    {
                        RoleAccessRights obj = new RoleAccessRights();
                        string[] arr = arrayRoleAccessRight[i].Split(',');                        
                        obj.MenuId = int.Parse(arr[0]);
                        obj.ShowMenu = true;
                        obj.ViewRight = bool.Parse(arr[1]);
                        obj.CreateRight = bool.Parse(arr[2]);
                        obj.EditRight = bool.Parse(arr[3]);
                        obj.DeleteRight = bool.Parse(arr[4]);

                        listUpdateRoleAccessRights.Add(obj);
                    }
                    _accessRightsServices.InsertUpdateRoleAccessRights(roleId, listUpdateRoleAccessRights, baseModel);
                    
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return Content("0");
            }
            return Content("1");
        }


        [NonAction]
        void BindDropdowns()
        {
            var roleList = _ddlService.GetRole(null);
            ViewBag.ddlRole = new SelectList(roleList, "id", "value");
        }
    }
}
