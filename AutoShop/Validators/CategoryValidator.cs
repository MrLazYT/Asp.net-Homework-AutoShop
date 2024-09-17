using DataAccess.Entities;
using FluentValidation;

namespace AutoShop.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(cat => cat.Name)
                .MinimumLength(10)
                .WithMessage("{PropertyName} length must be at least 10");

            RuleFor(car => car.Description)
                .MinimumLength(20)
                .WithMessage("{PropertyName} length must be at least 20");
        }
    }
}
