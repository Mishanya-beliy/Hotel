using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using System.Collections.Generic;

namespace Hotel.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _database;
        public CategoryService(IRepositoryManager database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            return _mapper.Map<List<CategoryDTO>>(_database.Categories.GetAll());
        }
        public CategoryDTO Get(int id)
        {
            var som = _mapper.Map<CategoryDTO>(_database.Categories.Get(id));
            return som;
        }

        public int Create(CategoryDTO category)
        {
            return _database.Categories.Create(_mapper.Map<Category>(category));
        }

        public bool Update(CategoryDTO category)
        {
            return _database.Categories.Update(category.ID,_mapper.Map<Category>(category));
        }

        public bool Delete(int id)
        {
            return _database.Categories.Delete(id);
        }
    }
}
