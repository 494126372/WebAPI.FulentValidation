using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication4.Models.Product;

namespace WebApplication4.Controllers
{
    public class ProductController : ApiController
    {
        // 不加过滤器的用法 
        public IHttpActionResult Post([FromBody]ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(model);
        }
    }
}



  