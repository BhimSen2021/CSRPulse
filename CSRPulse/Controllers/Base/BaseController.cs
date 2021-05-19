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

namespace CSRPulse.Controllers
{
    [CheckAuthorize] // it will check authorization on all the controller and its action which inherited by basecontroller
    public abstract class BaseController<T> : Controller where T : BaseController<T>
    {
        private ILogger<T> logger;

        protected ILogger<T> _logger => logger ??= HttpContext.RequestServices.GetService<ILogger<T>>();
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
    }
}