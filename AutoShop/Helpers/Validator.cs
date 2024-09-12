using DataAccess.Data;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AutoShop.Helpers
{
    public class Validator
    {
        private readonly CarContext _context;
        private readonly ModelStateDictionary _modelState;

        public Validator(CarContext context, ModelStateDictionary modelState)
        {
            _context = context;
            _modelState = modelState;
        }

        public bool[] IsCarValid(Car car)
        {
            bool areCarFieldsValid = AreFieldsValid(car);
            bool areFieldsChanged = AreFieldsChanged(car);

            if (areCarFieldsValid && areFieldsChanged && _modelState.IsValid)
            {
                return [true, true];
            }

            return [areCarFieldsValid, areFieldsChanged];
        }

        private bool AreFieldsValid(Car car)
        {
            return !String.IsNullOrEmpty(car.Model) &&
                   !String.IsNullOrEmpty(car.Color) &&
                   car.Year > 1900 &&
                   car.Year < 2100;
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
    }
}
