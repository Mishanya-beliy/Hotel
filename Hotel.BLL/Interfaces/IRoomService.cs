using Hotel.BLL.DTO;
using System;
using System.Collections.Generic;

namespace Hotel.BLL.Interfaces
{
    public interface IRoomService
    {
        IEnumerable<RoomDTO> GetAllRooms();
        RoomDTO Get(int id);
        public IEnumerable<RoomDTO> GetFreeRoomsOnDate(DateTime date);
        public bool CheckRoomOnDate(RoomDTO room, DateTime date);
    }
}
