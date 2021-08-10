using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.DAL.Repositories
{
    public class PriceRepository : IRepository<Price>
    {
        private HotelContext _db;

        public PriceRepository(HotelContext db)
        {
            _db = db;
        }

        public IEnumerable<Price> GetAll()
        {
            return _db.Prices;
        }
        public Price Get(int id)
        {
            return _db.Prices.FirstOrDefault(r => r.ID == id);
        }
        public int Create(Price price)
        {
            _db.Prices.Add(price);
            _db.SaveChanges();
            return price.ID;
        }
        public bool Update(int id, Price newPrice)
        {
            try
            {
                var oldPrice = Get(newPrice.ID);
                if (oldPrice is null)
                    return false;

                if (newPrice.Start != default && oldPrice.Start != newPrice.Start)
                    oldPrice.Start = newPrice.Start;
                if (newPrice.End != default && oldPrice.End != newPrice.End)
                    oldPrice.End = newPrice.End;
                if (newPrice.Coast != default && oldPrice.Coast != newPrice.Coast)
                    oldPrice.Coast = newPrice.Coast;

                _db.Prices.Update(oldPrice);
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
            var price = Get(id);
            if (price == null)
                return false;

            _db.Prices.Remove(price);
            _db.SaveChanges();
            return true;
        }
    }
}
