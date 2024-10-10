using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class CategoryService : IEntityService<CategoryDto>
    {
        private readonly CarContext _context;
        private readonly IMapper _mapper;

        public CategoryService(CarContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public CategoryDto GetById(int id)
        {
            DbSet<Category> categories = _context.Categories;
            Category category = categories.Find(id)!;

            return _mapper.Map<CategoryDto>(category);
        }

        public List<CategoryDto> GetAll()
        {
            DbSet<Category> categories  = _context.Categories;

            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public void Add(CategoryDto categoryDto)
        {
            Category category = _mapper.Map<Category>(categoryDto);

            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(CategoryDto categoryDto)
        {
            Category categoryOld = _context.Categories.FirstOrDefault(category => category.Id == categoryDto.Id)!;

            if (categoryOld != null)
            {
                Category categoryNew = _mapper.Map<Category>(categoryDto);

                _context.Categories.Update(categoryNew);
                _context.SaveChanges();
            }
        }

        public void Delete(CategoryDto categoryDto)
        {
            Category category = _mapper.Map<Category>(categoryDto);

            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
