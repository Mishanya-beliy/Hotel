using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hotel.DAL.EF
{
    public class HotelAuthenticationContext : IdentityDbContext<GuestIdentity>
    {
        public HotelAuthenticationContext(DbContextOptions<HotelAuthenticationContext> options) : base(options)
        {
        }
    }
}
