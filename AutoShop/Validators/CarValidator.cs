using DataAccess.Entities;
using FluentValidation;

namespace AutoShop.Validators
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(car => car.Model)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(car => car.Model)
                .MinimumLength(10)
                .WithMessage("{PropertyName} length must be at least 10");


            RuleFor(car => car.Color)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(car => car.Color)
                .MinimumLength(2)
                .WithMessage("{PropertyName} length must be at least 2");


            RuleFor(car => car.Color)
                .MaximumLength(30)
                .WithMessage("{PropertyName} length must be less than 30 characters");


            RuleFor(car => car.Year)
                .GreaterThan(1900)
                .LessThan(2100)
                .WithMessage("The {PropertyName} value must be between 1900 and 2100");


            RuleFor(car => car.Price)
                .GreaterThan(1)
                .LessThan(1000000);

            RuleFor(car => car.ImagePath)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(car => car.ImagePath)
                .Must(IsCorrectURI)
                .WithMessage("{PropertyName} is incorrect.");
        }

        private static bool IsCorrectURI(string uri)
        {
            return Uri.TryCreate(uri, UriKind.Absolute, out _);
        }
    }
}
