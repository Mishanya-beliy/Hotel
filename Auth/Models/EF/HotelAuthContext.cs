using Microsoft.EntityFrameworkCore;

namespace Auth.Models.EF
{
    public class HotelAuthContext : DbContext
    {
        public HotelAuthContext(DbContextOptions<HotelAuthContext> options) : base(options)
        {
        }

        public DbSet<AccountModel> Accounts { set; get; }

    }
}
