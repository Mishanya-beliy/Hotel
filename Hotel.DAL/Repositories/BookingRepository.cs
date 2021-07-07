using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using System.Collections.Generic;

namespace Hotel.DAL.Repositories
{
    public class BookingRepository : IRepository<Booking>
    {
        private HotelContext db;

        public BookingRepository(HotelContext db)
        {
            this.db = db;
        }

        public IEnumerable<Booking> GetAll()
        {
            return db.Bookings;
        }
        public Booking Get(int id)
        {
            return db.Bookings.Find(id);
        }
        public void Create(Booking booking)
        {
            db.Bookings.Add(booking);
            db.SaveChanges();
        }
        public bool Update(int id, Booking newBooking)
        {
            if (!Delete(id))
                return false;

            Create(newBooking);
            db.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            var booking = Get(id);
            if (booking == null)
                return false;

            db.Bookings.Remove(booking);
            db.SaveChanges();
            return true;
        }
    }
}
