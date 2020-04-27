using FluentValidation.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using AutomatskiPDFWebApi.Filters;
using AutomatskiPDFWebApi.Handlers;
using System.Web.Http.ExceptionHandling;
using AutomatskiPDFWebApi.ExLogger;

namespace AutomatskiPDFWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Filters.Add(new ValidateModelStateFilter());
            config.MessageHandlers.Add(new ResponseWrappingHandler());

            //Register Exception Handler  
            config.Services.Add(typeof(IExceptionLogger), new ExceptionManagerApi());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            FluentValidationModelValidatorProvider.Configure(config);
        }
    }
}
