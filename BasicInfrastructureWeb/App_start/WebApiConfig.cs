using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Cors;
using System.Web.Http.Dispatcher;
using BasicInfrastructure.Extensions;
using Newtonsoft.Json;

namespace BasicInfrastructureWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Configuração de filtros e atributos WebAPI
            config.Filters.Add(new HandleExceptionsAttribute());

            //Configuração de resposta do WebAPI
            ConfigureFormatters();

            //Configuração de injeção de dependência
            config.Services.Replace(typeof(IHttpControllerSelector), new CustomControllerSelector(config));
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{version}/{controller}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional,
                    version = "v1"
                },
                constraints: new { version = @"v\d+" }
            );

            config.Routes.MapHttpRoute(
                name: "UnversionedApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional
                }
            );
        }

        private static void ConfigureFormatters()
        {
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling =
                ReferenceLoopHandling.Ignore;

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(
                new RequestHeaderMapping("type", "json", StringComparison.InvariantCulture, false, new MediaTypeHeaderValue("application/json")));

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.MediaTypeMappings.Add(
                new RequestHeaderMapping("type", "xml", StringComparison.InvariantCulture, false, new MediaTypeHeaderValue("application/xml")));
        }
        public class CustomControllerSelector : DefaultHttpControllerSelector
        {
            private readonly HttpConfiguration _configuration;
            private readonly IEnumerable<Type> _controllerTypes;

            public CustomControllerSelector(HttpConfiguration configuration) : base(configuration)
            {
                _configuration = configuration;
                _controllerTypes = Assembly.GetExecutingAssembly().GetTypes()
                    .Where(i => typeof(IHttpController).IsAssignableFrom(i));
            }

            public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
            {
                var controllerName = GetControllerName(request);
                var version = request.GetRouteData().Values["version"]?.ToString();

                if (version == null)
                    return base.SelectController(request);

                var matchedController =
                    _controllerTypes.SingleOrDefault(i =>
                        Regex.IsMatch(i.FullName, $"(C|controllers([.]A|api)?[.]{version}[.])(?:\\w+[.])*?({controllerName}Controller$)",
                            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant));

                if (matchedController == null)
                    return base.SelectController(request);

                return new HttpControllerDescriptor(_configuration, controllerName, matchedController);
            }

        }

    }
}
