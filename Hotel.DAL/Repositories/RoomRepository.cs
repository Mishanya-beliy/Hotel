using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using System.Collections.Generic;

namespace Hotel.DAL.Repositories
{
    public class RoomRepository : IRepository<Room>
    {
        private HotelContext db;

        public RoomRepository(HotelContext db)
        {
            this.db = db;
        }

        public IEnumerable<Room> GetAll()
        {
            return db.Rooms;
        }
        public Room Get(int id)
        {
            return db.Rooms.Find(id);
        }
        public void Create(Room room)
        {
            db.Rooms.Add(room);
            db.SaveChanges();
        }
        public bool Update(int id, Room newRoom)
        {
            if (!Delete(id))
                return false;

            Create(newRoom);
            db.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            var room = Get(id);
            if (room == null)
                return false;

            db.Rooms.Remove(room);
            db.SaveChanges();
            return true;
        }
    }
}
