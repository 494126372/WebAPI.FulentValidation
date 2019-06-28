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
        // 不加过滤器的用法  不需要再全局Global中注册 需要在WebAPIConfig中加并添加注解 对所有方法接口奏效
        //MVC中不能这么用 必须全局注册下 并加注解    FluentValidationModelValidatorProvider.Configure();
        // 此接口也可以调用 验证因为全局注册了 !ModelState.IsValid 会主动调用
        public IHttpActionResult Post([FromBody]CustomProduct model)
        {
            if (!ModelState.IsValid) // 主动走RuleFor规则校验 这句话主动调用！！！！！
            {
                return BadRequest(ModelState);
            }
            return Ok(model);
        }
    }
}



  