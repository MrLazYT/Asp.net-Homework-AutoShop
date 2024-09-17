using DataAccess.Data;
using DataAccess.Entities;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AutoShop.Validators
{
    public class CarModelValidator
    {
        private readonly CarContext _context;
        private readonly ModelStateDictionary _modelState;
        private readonly CarValidator _validator;

        public CarModelValidator(CarContext context, ModelStateDictionary modelState)
        {
            _context = context;
            _modelState = modelState;
            _validator = new CarValidator();
        }

        public bool[] IsCarValid(Car car)
        {
            bool areCarFieldsValid = AreFieldsValid(car);
            bool areFieldsChanged = AreFieldsChanged(car);

            if (areCarFieldsValid && areFieldsChanged && _modelState.IsValid)
            {
                return new bool[2] { true, true };
            }

            //AddModelErrors(areCarFieldsValid, areFieldsChanged);

            return new bool[2] { areCarFieldsValid, areFieldsChanged };
        }

        private bool AreFieldsValid(Car car)
        {
            ValidationResult result = _validator.Validate(car);
            
            result.AddToModelState(_modelState);
            
            return result.IsValid;
        }

        private bool AreFieldsChanged(Car car)
        {
            Car sourceCar = _context.GetCarById(car.Id);

            if (sourceCar == null)
            {
                return true;
            }

            return car.Model != sourceCar.Model &&
                   car.Color != sourceCar.Color &&
                   car.Year != sourceCar.Year &&
                   car.CategoryId != sourceCar.CategoryId;
        }

        /*private void AddModelErrors(bool areCarFieldsValid, bool areFieldsChanged)
        {
            if (!areCarFieldsValid)
            {
                _modelState.AddModelError("Model", "Fields must be filled correctly");
            }

            if (!areFieldsChanged)
            {
                _modelState.AddModelError("Model", "Fields cannot be the same");
            }
        }*/
    }
}
