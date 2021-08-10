using Hotel.BLL.DTO;
using System.Collections.Generic;

namespace Hotel.BLL.Interfaces
{
    public interface IGuestService
    {
        IEnumerable<GuestDTO> GetAllGuests();
        GuestDTO Get(int id);
        int Registration(GuestDTO guest);
        public bool Update(GuestDTO guest);
        int GetIdByEmail(string email);
        bool Remove(int id);
        int Create(GuestDTO guest);
    }
}
