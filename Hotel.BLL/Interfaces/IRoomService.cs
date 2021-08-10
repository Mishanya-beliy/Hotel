using Hotel.BLL.DTO;
using System;
using System.Collections.Generic;

namespace Hotel.BLL.Interfaces
{
    public interface IRoomService
    {
        RoomDTO Get(int id);
        IEnumerable<RoomDTO> GetAllRooms();
        public IEnumerable<RoomDTO> GetFreeRooms(DateTime start, DateTime end, int countPeople);

        public bool Update(RoomDTO room);
        public int Create(RoomDTO room);
        bool Delete(int id);
    }
}
