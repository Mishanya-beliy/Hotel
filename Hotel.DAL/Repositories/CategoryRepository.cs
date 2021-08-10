using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.DAL.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private HotelContext _db;

        public CategoryRepository(HotelContext db)
        {
            _db = db;
        }

        public IEnumerable<Category> GetAll()
        {
            return _db.Categories;
        }

        public Category Get(int id)
        {
            var cat = _db.Categories.FirstOrDefault(r => r.ID == id);
            return cat;
        }
        public int Create(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            HotelInitializer.PriceInitialize(_db, new List<Category> { category });
            return category.ID;
        }
        public bool Update(int id, Category newCategory)
        {
            try
            {
                var oldCategory = Get(newCategory.ID);
                if (oldCategory is null)
                    return false;

                if (newCategory.Bed != default && oldCategory.Bed != newCategory.Bed)
                    oldCategory.Bed = newCategory.Bed;
                if (newCategory.MaxPeople != default && oldCategory.MaxPeople != newCategory.MaxPeople)
                    oldCategory.MaxPeople = newCategory.MaxPeople;
                if (oldCategory.Name != newCategory.Name)
                    oldCategory.Name = newCategory.Name;

                _db.Categories.Update(oldCategory);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            var category = Get(id);
            if (category == null)
                return false;

            _db.Categories.Remove(category);
            _db.SaveChanges();
            return true;
        }
    }
}
