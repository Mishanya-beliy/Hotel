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
        private readonly IRepositoryManager _database;
        private readonly IRoomService _roomService;

        public BookingService(IRepositoryManager database, IMapper mapper, IRoomService roomService)
        {
            _database = database;
            _mapper = mapper;
            _roomService = roomService;
        }

        public IEnumerable<BookingDTO> GetAllBookings()
        {
            return _mapper.Map<List<BookingDTO>>(_database.Bookings.GetAll());
        }


        public BookingDTO Get(int id)
        {
            return _mapper.Map<BookingDTO>(_database.Bookings.Get(id));
        }

        public void Booking(BookingDTO booking)
        {
            DateTime checkDate = booking.CheckIn;
            while(checkDate <= booking.CkeckOut)
            {
                if (!_roomService.CheckRoomOnDate(_roomService.Get(booking.RoomID), checkDate))
                    return;
                checkDate = checkDate.AddDays(1);
            }
            _database.Bookings.Create(_mapper.Map<Booking>(booking));
        }
    }
}
