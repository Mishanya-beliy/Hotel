using Hotel.DAL.Entities;

namespace Hotel.DAL.Interfaces
{
    public interface IRepositoryManager
    {
        IRepository<Guest> Guests { get; }
        IRepository<Booking> Bookings { get; }
        IRepository<Room> Rooms { get; }
        IRepository<Category> Categories { get; }
        IRepository<Price> Prices { get; }
        public void Save();
    }
}
