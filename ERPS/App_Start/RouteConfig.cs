using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                 name: "Default",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
             );
            
            routes.MapRoute(
                 name: "HR",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "HR", action = "GetHREmployees", id = UrlParameter.Optional }
             );
            routes.MapRoute(name: "AddEmployee",
              url: "AddEmployee",
              defaults: new { controller = "AddEmployee", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute(name: "DeleteEmployeeFinance",
             url: "DeleteEmployee",
             defaults: new { controller = "GetEmployeesFinance", action = "DeleteEmployeeFinance", id = UrlParameter.Optional });
            routes.MapRoute(name: "UpdateEmployeeFinance",
             url: "UpdateEmployee",
             defaults: new { controller = "GetEmployeesFinance", action = "UpdateEmployee", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "Finance",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Finance", action = "UpdateForm", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ParseJSONEmployee", 
                url: "ParseJSONEmployee",
                defaults: new {controller = "Management", action = "ParseJSONEmployee", id = UrlParameter.Optional }
            );
        }
    }
}
