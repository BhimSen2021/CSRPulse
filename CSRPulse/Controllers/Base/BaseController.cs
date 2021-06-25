using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using CSRPulse.Attributes;
using System;
using CSRPulse.Model;
using System.Collections.Generic;

namespace CSRPulse.Controllers
{
    [CheckAuthorize] // it will check authorization on all the controller and its action which inherited by basecontroller
    public abstract class BaseController<T> : Controller where T : BaseController<T>
    {
        private ILogger<T> logger;
        protected ILogger<T> _logger => logger ??= HttpContext.RequestServices.GetService<ILogger<T>>();

        protected UserDetail userDetail
        {
            get
            {
                UserDetail userDetail = HttpContext.Session.GetComplexData<UserDetail>("User");
                return userDetail;
            }
        }

        public ActionResult DownloadFile(string fileName, string fullPath, string sContentType)
        {
            if (System.IO.File.Exists(fullPath))
            {
                return File(fullPath, sContentType, fileName);
            }
            else
            {
                string sPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "your path here");
                byte[] fileBytes = System.IO.File.ReadAllBytes(sPath);
                return File(fileBytes, "image/jpg;base64");
            }
        }
        public ActionResult DownloadAnyFile(string sFileName, string sFileFullPath, string subDirectory = null)
        {
            if (System.IO.File.Exists(sFileFullPath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(sFileFullPath);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, sFileName);
            }
            else
            {
                string sPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "path");
                byte[] fileBytes = System.IO.File.ReadAllBytes(sPath);
                return File(fileBytes, "image/jpg;base64");
            }
        }

        [NonAction]
        public string ConvertViewToString<TModel>(string viewName, TModel model, bool partial = false)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = ControllerContext.ActionDescriptor.ActionName;
            }

            ViewData.Model = model;

            using var writer = new StringWriter();
            IViewEngine viewEngine = HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
            ViewEngineResult viewResult = viewEngine.FindView(this.ControllerContext, viewName, !partial);

            if (viewResult.Success == false)
            {
                return $"A view with the name {viewName} could not be found";
            }

            ViewContext viewContext = new ViewContext(
                ControllerContext,
                viewResult.View,
                ViewData,
                TempData,
                writer,
                new HtmlHelperOptions()
            );

            viewResult.View.RenderAsync(viewContext);

            return writer.ToString();
        }

        [NonAction]
        public string GenerateOTP()
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;
            characters += alphabets + numbers + small_alphabets;

            int length = 6;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }

        public async Task<IActionResult> DownloadFile(string filepath)
        {
            if (!System.IO.File.Exists(filepath))
                return Content($"Template not found.");

            var memory = new MemoryStream();
            using (var stream = new FileStream(filepath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filepath), Path.GetFileName(filepath));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

    }
}