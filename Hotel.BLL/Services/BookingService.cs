using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace Hotel.BLL.Services
{
    public class BookingService : IBookingService
    {
        private readonly IMapper _mapper;
        private readonly IPriceService _priceService;
        private readonly IRepositoryManager _database;

        public BookingService(IRepositoryManager database, IMapper mapper,
            IPriceService priceService)
        {
            _mapper = mapper;
            _database = database;
            _priceService = priceService;
        }

        public IEnumerable<BookingDTO> GetAllBookings()
        {
            return _mapper.Map<List<BookingDTO>>(_database.Bookings.GetAll());
        }


        public BookingDTO Get(int id)
        {
            return _mapper.Map<BookingDTO>(_database.Bookings.Get(id));
        }

        public ConfirmBookingDTO Booking(BookingDTO booking)
        {
            if (booking.CheckIn == DateTime.MinValue || booking.CkeckOut == DateTime.MinValue ||
                booking.CheckIn < DateTime.Now || booking.CheckIn > booking.CkeckOut)
                return null;

            int id = _database.Bookings.Create(_mapper.Map<Booking>(booking));
            if (id > 0)
            {
                booking = Get(id);
                if (booking is null)
                    return null;

                return new ConfirmBookingDTO($"{booking.Guest.Name} { booking.Guest.Surname }", booking.Room.Name, 
                    _priceService.CalculatePrice(booking.Room.Category, booking.CheckIn, booking.CkeckOut),
                    booking.CheckIn, booking.CkeckOut);
            }
            
            return null;
        }

        public bool Delete(int id)
        {
            return _database.Bookings.Delete(id);
        }

        public bool Update(int id, BookingDTO bookingDTO)
        {
            return _database.Bookings.Update(id, _mapper.Map<Booking>(bookingDTO));
        }
    }
}
