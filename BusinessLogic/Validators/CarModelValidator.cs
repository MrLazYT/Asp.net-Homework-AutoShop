using BusinessLogic.DTOs;
using BusinessLogic.Services;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BusinessLogic.Validators
{
    public class CarModelValidator
    {
        private readonly CarService _carService;
        private readonly ModelStateDictionary _modelState;
        private readonly CarValidator _validator;

        public CarModelValidator(CarService carService, ModelStateDictionary modelState)
        {
            _carService = carService;
            _modelState = modelState;
            _validator = new CarValidator();
        }

        public bool IsCarValid(CarDto car)
        {
            bool areCarFieldsValid = AreFieldsValid(car);

            if (areCarFieldsValid && _modelState.IsValid)
            {
                return true;
            }

            //AddModelErrors(areCarFieldsValid, areFieldsChanged);

            return areCarFieldsValid;
        }

        private bool AreFieldsValid(CarDto car)
        {
            ValidationResult result = _validator.Validate(car);
            
            result.AddToModelState(_modelState);
            
            return result.IsValid;
        }

       /* private bool AreFieldsChanged(CarDto car)
        {
            CarDto sourceCar = _carService.GetById(car.Id);

            if (sourceCar == null)
            {
                return true;
            }

            return car.Model != sourceCar.Model &&
                   car.Color != sourceCar.Color &&
                   car.Year != sourceCar.Year &&
                   car.CategoryId != sourceCar.CategoryId &&
                   car.Price != sourceCar.Price;
        }*/
    }
}
