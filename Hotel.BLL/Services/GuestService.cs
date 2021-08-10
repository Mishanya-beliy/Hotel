using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.BLL.Services
{
    public class GuestService : IGuestService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _database;

        public GuestService(IRepositoryManager database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public int Create(GuestDTO guest)
        {
            return _database.Guests.Create(guest);
        }

        public bool Update(GuestDTO guest)
        {
            if (Get(guest.ID) is null)
                return false;

            return _database.Guests.Update(guest.ID, _mapper.Map<Guest>(guest));
        }

        public int GetIdByEmail(string email)
        {
            var guests = GetAllGuests();

            return guests.FirstOrDefault(g => g.Email == email).ID;
        }

        public bool Remove(int id)
        {
            return _database.Guests.Delete(id);
        }

        public IEnumerable<GuestDTO> GetAllGuests()
        {
            return  _mapper.Map<List<GuestDTO>>(_database.Guests.GetAll());
        }

        public GuestDTO Get(int id)
        {            
            return _mapper.Map<GuestDTO>(_database.Guests.Get(id));
        }

        public int Registration(GuestDTO guest)
        {
            var guests = GetAllGuests();
            foreach (GuestDTO g in guests)
            {
                if (g.Email == guest.Email)
                    return -1;
                //if (g.Name == guest.Name)
                //    if (g.Surname == guest.Surname)
                //        if (g.Patronymic == guest.Patronymic)
                //            if (g.City == guest.City)
                //                if (g.Street == guest.Street)
                //                    return -1;
            }
            _database.Guests.Create(_mapper.Map<Guest>(guest));
            guests = GetAllGuests();
            
            return guests.FirstOrDefault(g => g.Email == guest.Email).ID;
        }
    }
}
