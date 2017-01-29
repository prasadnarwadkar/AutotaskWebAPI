using NSwag.AspNet.Owin;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AutotaskWebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            RouteTable.Routes.MapOwinPath("swagger", app =>
            {
                app.UseSwaggerUi(typeof(WebApiApplication).Assembly, new SwaggerUiOwinSettings
                {
                    MiddlewareBasePath = "/swagger"
                });
            });

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
        }
    }
}
