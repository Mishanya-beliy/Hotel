using Hotel.DAL.EF;
using Hotel.DAL.Entities.DbIncludeSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.DAL.Entities.IncludeFunctions
{
    class Include
    {
        //(, RoomSetting settings)
        public async static Task<IEnumerable<Room>> InRoom(DbSet<Room> entities, RoomSetting settings)
        {
            if (settings != null)
            {
                IEnumerable<Room> result = null;
                if (settings.Category)
                    if (settings.Price)
                        result = entities.Include(room => room.Category)
                            .ThenInclude(category => category.Prices);
                    else
                        result  = entities.Include(room => room.Category);


                if (true)//settings.Booking)
                    if (result != null)
                    {
                        var res = result as IIncludableQueryable<Room, Category>;
                        return res.Include(room => room.Bookings);
                    }
                    else
                        return entities.Include(room => room.Bookings);
                return result;
            }

            return entities;
        }

        public static async Task<IEnumerable<Price>> InPrice(DbSet<Price> entities, PriceSetting settings)
        {
            if(settings != null)
            {
                IIncludableQueryable<Price, ICollection<Category>> result = null;
                if (settings.Category)
                    result = entities
                        .Include(price => price.Categories);
                return result;
            }

            IIncludableQueryable<Price, ICollection<Category>> som = entities
                .Include(price => price.Categories);

            return await entities.ToListAsync();
        }

        public static async Task<IEnumerable<Guest>> InGuest(DbSet<Guest> entities, GuestSetting settings)
        {
            if (settings != null)
            {
                if (settings.Booking)
                    if (settings.Room)
                        entities
                            .Include(guest => guest.Bookings)
                            .ThenInclude(booking => booking.Room);
                    else
                        entities
                    .Include(guest => guest.Bookings);
            }

            return await entities.ToListAsync();
        }

        public static async Task<IEnumerable<Category>> InCategory(DbSet<Category> entities, CategorySetting settings)
        {
            if (settings != null)
            {
                if (settings.Price)
                    entities
                    .Include(guest => guest.Prices);
            }

            return await entities.ToListAsync();
        }
        public static async Task<IEnumerable<Booking>> InBooking(DbSet<Booking> entities, BookingSetting settings)
        {
            if (settings != null)
            {
                if (settings.Guest)
                    entities
                    .Include(booking => booking.Guest);

                if (settings.Room)
                    if (settings.Category)
                        if (settings.Price)
                            entities
                                .Include(booking => booking.Room)
                                .ThenInclude(room => room.Category)
                                .ThenInclude(category => category.Prices);
                        else
                            entities
                                .Include(booking => booking.Room)
                                .ThenInclude(room => room.Category);
                    else
                        entities
                            .Include(booking => booking.Room);
            }

            return await entities.ToListAsync();
        }
    }
}
