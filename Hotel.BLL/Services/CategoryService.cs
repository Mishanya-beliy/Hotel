using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
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
            return _mapper.Map<CategoryDTO>(_database.Categories.Get(id));
        }
    }
}
