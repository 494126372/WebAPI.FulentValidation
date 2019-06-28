using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace WebApplication4.Validators
{
    // 加过滤器的用法  此类不需要加 如果WEBAPI的话全局就会有效 这样写是为了新增和修改有效在Action上
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        //public override void OnActionExecuting(HttpActionContext actionContext)
        //{
        //    if (!actionContext.ModelState.IsValid)
        //    {
        //        //actionContext.ModelState.Keys
        //        actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
        //    }
        //    //base.OnActionExecuting(actionContext);

        //}
        // 重写Filter方法Action 拦截校验  然后在Action上座添加校验
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            bool vaildedtoken = false;

            if (!actionContext.ModelState.IsValid)
            {
                var errors = actionContext.ModelState.Values.Select(x => x.Errors);
                var count = errors.Count();
                List<string> errormsg = new List<string>();
                StringBuilder sb = new StringBuilder();
                foreach (var item in errors)
                {
                    for (int i = 0; i < item.Count; i++)
                    {
                        errormsg.Add(item[i].ErrorMessage);
                        sb.Append(item[i].ErrorMessage + "\n");
                    }

                }
                var response = new HttpResponseMessage();
                response.Content = new StringContent(string.Join("\n", errormsg.ToArray()));
                response.StatusCode = HttpStatusCode.BadRequest;
                throw new System.Web.Http.HttpResponseException(response);
                

            }




        }
       
    }
    
}
   