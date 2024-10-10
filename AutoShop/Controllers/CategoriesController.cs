using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Validators;
using FluentValidation.Results;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using BusinessLogic.Services;
using BusinessLogic.DTOs;

namespace AutoShop.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class CategoriesController : Controller
    {
        private readonly CategoryService _categoryService;
        private readonly CategoryValidator _validator;

        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
            _validator = new CategoryValidator();
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_categoryService.GetAll());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryService.GetAll()
                .FirstOrDefault(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Description")] CategoryDto category)
        {
            ValidationResult validationResult = _validator.Validate(category);
            validationResult.AddToModelState(ModelState);
            
            if (ModelState.IsValid && validationResult.IsValid)
            {
                _categoryService.Add(category);
                
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryService.GetAll().Find(category => category.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Description")] CategoryDto category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            ValidationResult validationResult = _validator.Validate(category);
            validationResult.AddToModelState(ModelState);

            if (ModelState.IsValid && validationResult.IsValid)
            {
                try
                {
                    _categoryService.Update(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryService.GetAll()
                .FirstOrDefault(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _categoryService.GetAll().Find(category => category.Id == id);
            if (category != null)
            {
                _categoryService.Delete(category);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _categoryService.GetAll().Any(e => e.Id == id);
        }
    }
}
