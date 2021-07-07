using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using System.Collections.Generic;

namespace Hotel.DAL.Repositories
{
    public class GuestRepository : IRepository<Guest>
    {
        private readonly HotelContext _db;

        public GuestRepository(HotelContext db)
        {
            _db = db;
        }

        public IEnumerable<Guest> GetAll()
        {
            return _db.Guests;
        }
        public Guest Get(int id)
        {
            return _db.Guests.Find(id);
        }
        public void Create(Guest guest)
        {
            _db.Guests.Add(guest);
            _db.SaveChanges();
        }
        public bool Update(int id, Guest newGuest)
        {
            if (!Delete(id))
                return false;
            
            Create(newGuest);
            _db.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            var guest = Get(id);
            if (guest == null)
                return false;

            _db.Guests.Remove(guest);
            _db.SaveChanges();
            return true;
        }
    }
}
