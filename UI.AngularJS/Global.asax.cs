using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using JavaScriptEngineSwitcher.Core;

namespace UI.AngularJS
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            JsEngineSwitcherConfig.Configure(JsEngineSwitcher.Instance);
        }

        protected void Application_EndRequest()
        {
            Response.AddHeader("X-UA-Compatible", "IE=edge,chrome=1");
        }
    }
}