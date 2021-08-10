using Hotel.BLL.DTO;
using System.Collections.Generic;

namespace Hotel.BLL.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetAllCategories();
        CategoryDTO Get(int id);
        int Create(CategoryDTO categoryDTO);
        bool Update(CategoryDTO categoryDTO);
        bool Delete(int id);
    }
}
