using Hotel.DAL.Entities;
using Hotel.DAL.Entities.DbIncludeSettings;

namespace Hotel.DAL.Interfaces
{
    public interface IRepositoryManager
    {
        IRepository<Room> Rooms { get; }
        IRepository<Guest> Guests { get; }
        IRepository<Booking> Bookings { get; }
        IRepository<Category> Categories { get; }
        IRepository<Price> Prices { get; }
        public void Save();
    }
}
