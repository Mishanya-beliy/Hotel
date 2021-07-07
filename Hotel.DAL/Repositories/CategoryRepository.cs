using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using System.Collections.Generic;

namespace Hotel.DAL.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private HotelContext db;

        public CategoryRepository(HotelContext db)
        {
            this.db = db;
        }

        public IEnumerable<Category> GetAll()
        {
            return db.Categories;
        }
        public Category Get(int id)
        {
            return db.Categories.Find(id);
        }
        public void Create(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
        }
        public bool Update(int id, Category newCategory)
        {
            if (!Delete(id))
                return false;

            Create(newCategory);
            db.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            var category = Get(id);
            if (category == null)
                return false;

            db.Categories.Remove(category);
            db.SaveChanges();
            return true;
        }
    }
}
