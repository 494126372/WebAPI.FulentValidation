using System;
using System.Collections.Generic;
using System.Linq;
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

        [ValidateModelStateFilter]   // // 加过滤器的用法  
        public ActionResult Create([System.Web.Http.FromBody]ProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
               
               var errors = ModelState.Values.Select(x => x.Errors).First().ToString();
                ViewBag.err = errors == "" ? "无错误" : errors ;
                  
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
