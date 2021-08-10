using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Entities.DbIncludeSettings;
using Hotel.DAL.Entities.IncludeFunctions;
using Hotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.DAL.Repositories
{
    public class HelpRepository
    {
        private HotelContext _db;

        public static bool CheckRoomOnDate(Room room, DateTime date)
        {
            foreach (Booking booking in room.Bookings)
                if (booking.CkeckOut > date)
                    if (booking.CheckIn < date)
                        return false;

            return true;
        }
        public IEnumerable<Booking> GetAll()
        {
            return _db.Bookings;
        }

        public Booking Get(int id)
        {
            return _db.Bookings.FirstOrDefault(r => r.ID == id);
        }
        public int Create(Booking booking)
        {
            int id = -1;

            if (_db.Rooms.Find(booking.RoomID) is null)
                return id;

            try
            {
                _db.Bookings.Add(booking);
                _db.SaveChanges();
            }
            catch
            {
                return -1;
            }
            return booking.ID;
        }
        public bool Update(int id, Booking newBooking)
        {
            if (!Delete(id))
                return false;

            Create(newBooking);
            _db.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            var booking = Get(id);
            if (booking == null)
                return false;

            _db.Bookings.Remove(booking);
            _db.SaveChanges();
            return true;
        }
    }
}
