using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication4.Validators;
using WebApplication4.Validators.Product;

namespace WebApplication4.Models.Product
{
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
}