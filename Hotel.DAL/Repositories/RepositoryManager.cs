using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Entities.DbIncludeSettings;
using Hotel.DAL.Interfaces;

namespace Hotel.DAL.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly HotelContext _db;
        private GuestRepository guestRepository;
        private BookingRepository bookingRepository;
        private RoomRepository roomRepository;
        private CategoryRepository categoryRepository;
        private PriceRepository priceRepository;

        public RepositoryManager(HotelContext db)
        {
            _db = db;
        }

        public IRepository<Guest> Guests
        {
            get
            {
                if (guestRepository == null)
                {
                    guestRepository = new GuestRepository(_db);
                }

                return guestRepository;
            }
        }

        public IRepository<Booking> Bookings
        {
            get
            {
                if (bookingRepository == null)
                {
                    bookingRepository = new BookingRepository(_db);
                }

                return bookingRepository;
            }
        }
        public IRepository<Room> Rooms
        {
            get
            {
                if (roomRepository == null)
                {
                    roomRepository = new RoomRepository(_db);
                }

                return roomRepository;
            }
        }
        public IRepository<Category> Categories
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new CategoryRepository(_db);
                }

                return categoryRepository;
            }
        }
        public IRepository<Price> Prices
        {
            get
            {
                if (priceRepository == null)
                {
                    priceRepository = new PriceRepository(_db);
                }

                return priceRepository;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
