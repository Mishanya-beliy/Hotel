using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Entities.DbIncludeSettings;
using Hotel.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.DAL.Repositories
{
    public class RoomRepository : IRepository<Room>
    {
        private HotelContext _db;

        public RoomRepository(HotelContext db)
        {
            _db = db;
        }

        public IEnumerable<Room> GetAll()
        {
            return _db.Rooms;
        }
        public Room Get(int id)
        {
            return _db.Rooms.FirstOrDefault(r => r.ID == id);
        }
        public int Create(Room room)
        {
            room.Category = null;
            _db.Rooms.Add(room);
            _db.SaveChanges();
            return room.ID;
        }
        public bool Update(int id, Room newRoom)
        {
            try
            {
                var oldRoom = Get(newRoom.ID);
                if (oldRoom is null)
                    return false;

                if (newRoom.Name != default && oldRoom.Name != newRoom.Name)
                    oldRoom.Name = newRoom.Name;
                if (newRoom.CategoryID != default && oldRoom.CategoryID != newRoom.CategoryID)
                    oldRoom.CategoryID = newRoom.CategoryID;

                _db.Rooms.Update(oldRoom);
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
            var room = Get(id);
            if (room == null)
                return false;

            _db.Rooms.Remove(room);
            _db.SaveChanges();
            return true;
        }
    }
}
