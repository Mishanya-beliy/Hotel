using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.BLL.Services
{
    public class RoomService : IRoomService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _database;

        public RoomService(IRepositoryManager database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public IEnumerable<RoomDTO> GetAllRooms()
        {
            return _mapper.Map<List<RoomDTO>>(_database.Rooms.GetAll());
        }


        public RoomDTO Get(int id)
        {
            return _mapper.Map<RoomDTO>(_database.Rooms.Get(id));
        }

        public IEnumerable<RoomDTO> GetFreeRoomsOnDate(DateTime date)
        {
            List<RoomDTO> rooms = GetAllRooms().ToList();
            foreach(RoomDTO room in rooms)
                if (!CheckRoomOnDate(room, date)) 
                    rooms.Remove(room);

            return rooms;
        }
        public bool CheckRoomOnDate(RoomDTO room, DateTime date)
        {
            foreach (BookingDTO booking in room.Bookings)
            {
                if (booking.CkeckOut > date)
                    if (booking.CheckIn < date)
                        return false;
            }
            return true;
        }
    }
}
