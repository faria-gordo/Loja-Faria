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
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Web",
                routeTemplate: "{controller}/{action}/{data}",
                defaults: new { data= RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "Dash",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
