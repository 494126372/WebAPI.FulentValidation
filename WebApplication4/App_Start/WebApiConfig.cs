using FluentValidation.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApplication4.Validators;

namespace WebApplication4
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var jsonFormater = config.Formatters.JsonFormatter;
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //安装完成后,您需要在应用程序的启动例程中配置
            //如果要让这个过滤器对所有的Controller都起作用，请在WebApiConfig中注册全局过滤
            //config.Filters.Add(new ValidateModelStateFilter());

            // Web API routes WebApi 下载下面的 
            //Install - Package FluentValidation.WebApi

            //这个是WEBAPI加的地方 添加之后全局拦截
            FluentValidationModelValidatorProvider.Configure(config);

        }
    }
}
