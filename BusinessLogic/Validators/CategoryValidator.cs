using BusinessLogic.DTOs;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryDto>
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