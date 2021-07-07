using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using System.Collections.Generic;

namespace Hotel.DAL.Repositories
{
    public class PriceRepository : IRepository<Price>
    {
        private HotelContext db;

        public PriceRepository(HotelContext db)
        {
            this.db = db;
        }

        public IEnumerable<Price> GetAll()
        {
            return db.Prices;
        }
        public Price Get(int id)
        {
            return db.Prices.Find(id);
        }
        public void Create(Price price)
        {
            db.Prices.Add(price);
            db.SaveChanges();
        }
        public bool Update(int id, Price newPrice)
        {
            if (!Delete(id))
                return false;

            Create(newPrice);
            db.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            var price = Get(id);
            if (price == null)
                return false;

            db.Prices.Remove(price);
            db.SaveChanges();
            return true;
        }
    }
}
