using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Entities.DbIncludeSettings;
using Hotel.DAL.Entities.IncludeFunctions;
using Hotel.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.DAL.Repositories
{
    public class GuestRepository : IRepository<Guest>
    {
        private readonly HotelContext _db;

        public GuestRepository(HotelContext db)
        {
            _db = db;
        }

        public IEnumerable<Guest> GetAll()
        {
            return _db.Guests;
        }
        public Guest Get(int id)
        {
            return _db.Guests.FirstOrDefault(r => r.ID == id);
        }
        public int Create(Guest guest)
        {
            _db.Guests.Add(guest);
            _db.SaveChanges();
            return guest.ID;
        }
        public bool Update(int id, Guest newGuest)
        {
            try
            {
                var oldGuest = Get(newGuest.ID);
                if (oldGuest is null)
                    return false;

                if (newGuest.Name != default && oldGuest.Name != newGuest.Name)
                    oldGuest.Name = newGuest.Name;

                if (newGuest.Surname != default && oldGuest.Surname != newGuest.Surname)
                    oldGuest.Surname = newGuest.Surname;

                if (newGuest.Patronymic != default && oldGuest.Patronymic != newGuest.Patronymic)
                    oldGuest.Patronymic = newGuest.Patronymic;

                if (newGuest.DateOfBirth != default && oldGuest.DateOfBirth != newGuest.DateOfBirth)
                    oldGuest.DateOfBirth = newGuest.DateOfBirth;

                if (newGuest.Email != default && oldGuest.Email != newGuest.Email)
                    oldGuest.Email = newGuest.Email;

                if (newGuest.City != default && oldGuest.City != newGuest.City)
                    oldGuest.City = newGuest.City;

                if (newGuest.Street != default && oldGuest.Street != newGuest.Street)
                    oldGuest.Street = newGuest.Street;

                if (newGuest.House != default && oldGuest.House != newGuest.House)
                    oldGuest.House = newGuest.House;

                if (newGuest.Apartment != default && oldGuest.Apartment != newGuest.Apartment)
                    oldGuest.Apartment = newGuest.Apartment;

                _db.Guests.Update(oldGuest);
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
            var guest = Get(id);
            if (guest == null)
                return false;

            _db.Guests.Remove(guest);
            _db.SaveChanges();
            return true;
        }
    }
}
