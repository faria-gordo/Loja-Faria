using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Loja.Services
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Web",
                routeTemplate: "{controller}/{action}/{data}",
                defaults: new { data = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "Dash",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "Cart",
                routeTemplate: "{controller}/{action}/{identifier}",
                defaults: new { identifier = RouteParameter.Optional }
            );
            //config.Routes.MapHttpRoute(
            //    name: "User",
            //    routeTemplate: "{controller}/{action}/{userInfo}",
            //    defaults: new { userInfo = RouteParameter.Optional }
            //);
        }
    }
}
