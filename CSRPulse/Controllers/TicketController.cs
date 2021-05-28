using CSRPulse.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Controllers
{
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Ticket()
        {
            return await Task.FromResult((IActionResult)PartialView("_Ticket", new Ticket()));
        }

    }
}
