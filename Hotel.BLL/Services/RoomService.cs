using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Entities;
using Hotel.DAL.Entities.DbIncludeSettings;
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

        public int Create(RoomDTO room)
        {
            return _database.Rooms.Create(_mapper.Map<Room>(room));

        }
        public bool Update(RoomDTO room)
        {
            var id = room.ID;
            //room.Category = null;
            //room.Bookings = null;
            return _database.Rooms.Update(id, _mapper.Map<Room>(room));
        }


        public RoomDTO Get(int id)
        {
            return _mapper.Map<RoomDTO>(_database.Rooms.Get(id));
        }

        public IEnumerable<RoomDTO> GetAllRooms()
        {
            return _mapper.Map<List<RoomDTO>>(_database.Rooms.GetAll());
        }
        public IEnumerable<RoomDTO> GetFreeRooms(DateTime start, DateTime end, int countPeople)
        {
            var rooms = GetAllRooms();
            //for (DateTime day = start.Date; day < end; day = day.AddDays(1))
            //    rooms = GetFreeRoomsOnDate(day, rooms, countPeople);

            var freeRooms = (from room in rooms
                      where room.Category.MaxPeople >= countPeople
                      && !room.Bookings.Where(booking => (booking.CheckIn >= start && booking.CheckIn <= end)
                      || (booking.CkeckOut >= start && booking.CkeckOut <= end)).Any()
                      select room).ToList();

            return freeRooms;
        }

        private List<RoomDTO> GetFreeRoomsOnDate(DateTime date, List<RoomDTO> rooms, int people)
        {
            foreach(RoomDTO room in rooms)
                if(room.Category.MaxPeople < people || !CheckRoomOnDate(room, date))
                    rooms.Remove(room);

            return rooms;
        }
        private bool CheckRoomOnDate(RoomDTO room, DateTime date)
        {
            foreach (BookingDTO booking in room.Bookings)
                if (booking.CkeckOut > date)
                    if (booking.CheckIn < date)
                        return false;

            return true;
        }

        public bool Delete(int id)
        {
            return _database.Rooms.Delete(id);
        }
    }
}
