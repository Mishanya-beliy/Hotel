using Microsoft.AspNetCore.Identity;

namespace Hotel.DAL.EF
{
    public class GuestIdentity : IdentityUser
    {
        [PersonalData]
        public int GuestID { set; get; }


        public class ClaimType
        {
            public const string Id = "Id";
            public const string Name = "Name";
            public const string Role = "Role";
            public const string GuestId = "GuestId";
        }
    }
}
