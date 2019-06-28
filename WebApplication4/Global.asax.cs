using FluentValidation;
using FluentValidation.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApplication4
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           
            //设置 FluentValidation 默认的资源文件提供程序 - 中文资源
    //ValidatorOptions.ResourceProviderType = typeof(FluentValidationResource);

    ///* 比如验证用户名 not null、not empty、length(2,int.MaxValue) 时，链式验证时，如果第一个验证失败，则停止验证 */
    //ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure; // ValidatorOptions.CascadeMode 默认值为：CascadeMode.Continue


    // 配置 FluentValidation 模型验证为默认的 ASP.NET MVC 模型验证 MVC下载  Install-Package FluentValidation.Mvc5 
    FluentValidationModelValidatorProvider.Configure();
        }
    }
}
