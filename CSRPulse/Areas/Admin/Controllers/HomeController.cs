using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[action]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //var startupPath = Directory.GetCurrentDirectory();
            //var drive = startupPath.Substring(0, 1);
            //DriveInfo dInfo = new DriveInfo(drive);
            //if (dInfo.IsReady)
            //{
            //    ViewBag.DriveInfo = dInfo;
            //}
            return View();
        }
    }
}
