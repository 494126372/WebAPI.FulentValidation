using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication4.Models.Product;

namespace WebApplication4.Validators.Product
{
    public class ProductValidator : BaseValidator<ProductViewModel>
    {
        //public ProductValidator()
        //{
        //    RuleFor(s => s.Code).Length(6, 32).WithMessage("长度需在{0} 至 {1} 个字符之间");
        //    RuleFor(s => s.Name).Length(2, 128).WithMessage("长度需在{0} 至 {1} 个字符之间");
        //    RuleFor(s => s.Price).GreaterThan(0).WithMessage("价格必须大于0");
        //}
        //这个可以直接卸载ViewModel中
    }
}