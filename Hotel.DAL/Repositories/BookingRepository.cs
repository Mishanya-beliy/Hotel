using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Entities.DbIncludeSettings;
using Hotel.DAL.Entities.IncludeFunctions;
using Hotel.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.DAL.Repositories
{
    public class BookingRepository : IRepository<Booking>
    {
        private HotelContext _db;

        public BookingRepository(HotelContext db)
        {
            _db = db;
        }


        public IEnumerable<Booking> GetAll()
        {
            return _db.Bookings;
        }

        public Booking Get(int id)
        {
            return _db.Bookings.Include(b => b.Guest).FirstOrDefault(r => r.ID == id);
        }
        public int Create(Booking booking)
        {
            int id = -1;

            var room = _db.Rooms.Find(booking.RoomID);
            if (room is null)
                return id;

            for (DateTime current = booking.CheckIn; current <= booking.CkeckOut; current = current.AddDays(1))
                if (!HelpRepository.CheckRoomOnDate(room, current))
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
            try
            {
                var oldBooking = Get(newBooking.ID);
                if (oldBooking is null)
                    return false;

                if (newBooking.CheckIn != default && oldBooking.CheckIn != newBooking.CheckIn)
                    oldBooking.CheckIn = newBooking.CheckIn;

                if (newBooking.CkeckOut != default && oldBooking.CkeckOut != newBooking.CkeckOut)
                    oldBooking.CkeckOut = newBooking.CkeckOut;

                if (newBooking.GuestID != default && oldBooking.GuestID != newBooking.GuestID)
                    oldBooking.GuestID = newBooking.GuestID;

                if (newBooking.RoomID != default && oldBooking.RoomID != newBooking.RoomID)
                    oldBooking.RoomID = newBooking.RoomID;

                _db.Bookings.Update(oldBooking);
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
            var booking = Get(id);
            if (booking == null)
                return false;

            _db.Bookings.Remove(booking);
            _db.SaveChanges();
            return true;
        }
    }
}
