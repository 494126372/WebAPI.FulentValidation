using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Validators
{
    public abstract class BaseValidator<TModel> : AbstractValidator<TModel> where TModel : class
    {
    }
}