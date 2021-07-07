using Hotel.BLL.DTO;
using System.Collections.Generic;

namespace Hotel.BLL.Interfaces
{
    public interface IGuestService
    {
        IEnumerable<GuestDTO> GetAllGuests();
        GuestDTO Get(int id);
        void Registration(GuestDTO guest);
    }
}
