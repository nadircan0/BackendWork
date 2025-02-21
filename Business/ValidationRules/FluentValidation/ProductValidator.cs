using System;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

public class ProductValidator:AbstractValidator<Product>
{

    public ProductValidator()
    {
        RuleFor(p => p.ProductName).MinimumLength(2);
        RuleFor(p => p.ProductName).NotEmpty();
        RuleFor(p => p.UnitPrice).GreaterThan(0);
        RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("The products should start whith A");

        RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);

    }

    private bool StartWithA(string arg)
    {
        return arg.StartsWith("A");
    }
}
