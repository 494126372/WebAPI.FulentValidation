
## Mvc 5 的校验： 参考地址 https://fluentvalidation.net/mvc5

+  配置安装Nuget命令行安装 
+  Install-Package FluentValidation.Mvc5 
+ WebAPI 得安装 Install-Package FluentValidation.WebApi

+ 混合都得安装 
+  Mvc 可以在 Global.cs 全局配置 FluentValidation 模型验证为默认的 ASP.NET MVC 模型验证 MVC下载  Install-Package FluentValidation.Mvc5 
    FluentValidationModelValidatorProvider.Configure(); 

+ Mvc 中添加类继承抽象类：
+  public abstract class BaseValidator<TModel> : AbstractValidator<TModel> where TModel : class
    {
    }
+ 然后创建ProductViewModel  ProductValidator继承抽象泛型 
+ 不想改动原来实体的可做继承扩展 也可加注解的校验

``
  [Validator(typeof(ProductValidator))] // 添加上之后就可以对标记的model进行校验了
    public class ProductViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
    }
    public class CustomProduct: ProductViewModel  //一般不在原型修改 直接继承
    {
        public string Tel { get; set; }
    }

    public class ProductValidator : BaseValidator<CustomProduct>
    {
        // 校验规则如下
        public ProductValidator()
        {
            RuleFor(s => s.Code).Length(6, 32).WithMessage("长度需在{0} 至 {1} 个字符之间");
            RuleFor(s => s.Name).Length(2, 128).WithMessage("长度需在{0} 至 {1} 个字符之间");
            RuleFor(s => s.Price).GreaterThan(0).WithMessage("价格必须大于0");
            RuleFor(s => s.Name).NotEmpty().WithMessage("名字不能为空！");
            string regexstr = "^1[3456789][0-9]\\d{8}$"; 
            string regexstr2 = @"^(([0\+]\d{2,3}-)?(0\d{2,3})-)?(\d{7,8})(-(\d{3,}))?$";
            RuleFor(s => s.Tel).Matches(regexstr).WithMessage("货主的手机号格式有误！");

        }
    }

	``

	+ 下面是Action 错误处理  ModelState.IsValid 会主动跳进校验规则校验
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
	+ WebAPI写法跟上面差不多

	``
	   public IHttpActionResult Post([FromBody]CustomProduct model)
        {
            if (!ModelState.IsValid) // 主动走RuleFor规则校验 这句话主动调用！！！！！
            {
                return BadRequest(ModelState);
            }
            return Ok(model);
        }
	
	``
	+  以上是会返回json错误 信息可使用ViewBag进行接收前端处理 
	+   下面是官方的写法：
	``
		public ActionResult Create() {
			return View();
		}
 
		[HttpPost]
		public ActionResult Create(Person person) {
 
			if(! ModelState.IsValid) { // re-render the view when validation failed.
				return View("Create", person);
			}
 
			TempData["notice"] = "Person successfully created";
			return RedirectToAction("Index");
 
		}
	``

	+ 官方的前端验证 

	``
	@Html.ValidationSummary()
 
	@using (Html.BeginForm()) {
		Id: @Html.TextBoxFor(x => x.Id) @Html.ValidationMessageFor(x => x.Id)
		<br />
		Name: @Html.TextBoxFor(x => x.Name) @Html.ValidationMessageFor(x => x.Name) 		
		<br />
		Email: @Html.TextBoxFor(x => x.Email) @Html.ValidationMessageFor(x => x.Email)
		<br />
		Age: @Html.TextBoxFor(x => x.Age) @Html.ValidationMessageFor(x => x.Age)
 
		<br /><br />
 
		<input type="submit" value="submit" />
	}

``

+ 但是页面过多就得每个Action都得去加 !ModelState.IsValid 验证判断 简单做法就是 使用ActionFilter拦截器 全局添加处理
+ 错误的方法 替换!ModelState.IsValid 判断处理方法 
+ 代码如下：
``
   public class ValidateModelStateFilter : ActionFilterAttribute
    {
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

	``
	+ 然后在Action或者 APi 接口加上 注解拦截器 [ValidateModelStateFilter]
	+ 此拦截器可在类路由器加或者Action方法api方法加 具体看需求 一般添加 新增和修改添加就可以了