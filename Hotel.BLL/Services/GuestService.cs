using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using System.Collections.Generic;

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

        public IEnumerable<GuestDTO> GetAllGuests()
        {
            return _mapper.Map<List<GuestDTO>>(_database.Guests.GetAll());
        }


        public GuestDTO Get(int id)
        {
            return _mapper.Map<GuestDTO>(_database.Guests.Get(id));
        }

        public void Registration(GuestDTO guest)
        {
            var guests = GetAllGuests();
            foreach (GuestDTO g in guests)
            {
                if (g.Name == guest.Name)
                    if (g.Surname == guest.Surname)
                        if (g.Patronymic == guest.Patronymic)
                            if (g.City == guest.City)
                                if (g.Street == guest.Street)
                                    return;
            }
            _database.Guests.Create(_mapper.Map<Guest>(guest));
        }
    }
}
