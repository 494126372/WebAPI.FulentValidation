using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models.Product;
using WebApplication4.Validators;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        [HttpPost]  // 不加过滤器的用法  
        //[ValidateAntiForgeryToken]

        [ValidateModelStateFilter]   // // 加过滤器的用法  必须在Global中注册
        public ActionResult Create([System.Web.Http.FromBody]CustomProduct product)
        {
            //CustomerValidator validator = new CustomerValidator();
            //ValidationResult results = validator.Validate(customer);
            if (!ModelState.IsValid) ///此句话很重要 加上会不用过滤器直接走校验规则坏处是每个Action都要写一遍 所以添加全局验证Filter拦截中写上统一的
            {
                var errors = ModelState.Values.Select(x => x.Errors).First().ToString();
                ViewBag.err = errors == "" ? "无错误" : errors;

                var response = new HttpResponseMessage();
                response.Content = new StringContent(string.Join("\n", errors));
                response.StatusCode = HttpStatusCode.BadRequest;

                throw new System.Web.Http.HttpResponseException(response);
            }
            return View(product);
        }
        [HttpGet]  // 不加过滤器的用法 
      
        public ActionResult Create()
        {
         
            return View();
        }
    }
}
