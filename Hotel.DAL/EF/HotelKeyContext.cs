using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hotel.DAL.EF
{
    public class HotelKeyContext : DbContext, IDataProtectionKeyContext
    {
        public HotelKeyContext(DbContextOptions<HotelKeyContext> options) 
            : base(options) { }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    }
}
