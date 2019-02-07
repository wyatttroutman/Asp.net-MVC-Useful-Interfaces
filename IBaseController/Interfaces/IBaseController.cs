using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Project.Interfaces
{
    public class IBaseController : Controller
    {
        // Function to set up modal pup with Error styling
        public void Error(string errorHeader, string errorMessage)
        {
            ViewBag.ErrorHeader = errorHeader;
            ViewBag.ErrorMessage = errorMessage;
            ViewBag.JavaScriptHandler = "ShowErrorModal();";
        }

        // Function to set up modal pup with Error styling + show exception details
        public void Error(string errorHeader, string errorMessage, ref Exception exception)
        {
            ViewBag.ErrorHeader = errorHeader;
            ViewBag.ErrorMessage = errorMessage + "<br />" + exception.Message + "<br />" + "<br />" + exception.StackTrace;
            ViewBag.JavaScriptHandler = "ShowErrorModal();";
        }


        // Function to set up modal pup with Informational styling
        public void Info(string infoHeader, string infoMessage)
        {
            ViewBag.InfoHeader = infoHeader;
            ViewBag.InfoMessage = infoMessage;
            ViewBag.JavaScriptHandler = "ShowInfoModal();";
        }

        // Function to override OnException method to pass exception info the ErrorController
        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            
            //Redirect or return a view, but not both.
            filterContext.Result = RedirectToAction("Message", "ErrorHandler", new { area = "", errorException = filterContext.Exception.Message });
        }

        internal string PartialViewToString(string partialViewName, object model = null)
        {
            ControllerContext controllerContext = new ControllerContext(Request.RequestContext, this);

            return ViewToString(
                controllerContext,
                ViewEngines.Engines.FindPartialView(controllerContext, partialViewName) ?? throw new FileNotFoundException("Partial view cannot be found."),
                model
            );
        }

        protected string ViewToString(string viewName, object model = null)
        {
            ControllerContext controllerContext = new ControllerContext(Request.RequestContext, this);

            return ViewToString(
                controllerContext,
                ViewEngines.Engines.FindView(controllerContext, viewName, null) ?? throw new FileNotFoundException("View cannot be found."),
                model
            );
        }

        protected string ViewToString(string viewName, string controllerName, string areaName, object model = null)
        {
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", controllerName);

            if (areaName != null)
            {
                routeData.Values.Add("Area", areaName);
                routeData.DataTokens["area"] = areaName;
            }

            ControllerContext controllerContext = new ControllerContext(HttpContext, routeData, this);

            return ViewToString(
                controllerContext,
                ViewEngines.Engines.FindView(controllerContext, viewName, null) ?? throw new FileNotFoundException("View cannot be found."),
                model
            );
        }
        private string ViewToString(ControllerContext controllerContext, ViewEngineResult viewEngineResult, object model)
        {
            using (StringWriter writer = new StringWriter())
            {
                ViewContext viewContext = new ViewContext(
                    ControllerContext,
                    viewEngineResult.View,
                    new ViewDataDictionary(model),
                    new TempDataDictionary(),
                    writer
                );

                viewEngineResult.View.Render(viewContext, writer);

                return writer.ToString();
            }
        }
    }
}