using Hotel.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel.DAL.EF
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> options) : base(options)
        {
        }

        public DbSet<Guest> Guests { set; get; }
        public DbSet<Booking> Bookings { set; get; }
        public DbSet<Room> Rooms { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<Price> Prices { set; get; }
    }
}
