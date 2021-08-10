using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using static Hotel.DAL.EF.GuestIdentity;

namespace Hotel.WEB.Additional
{
    public class User
    {
        internal static string GetUserIdFromClaims(IEnumerable<Claim> claims)
        {
            return GetFromClaims(ClaimType.Id, claims);
        }

        internal static int GetGuestIdFromClaims(IEnumerable<Claim> claims)
        {
            var id = GetFromClaims(ClaimType.GuestId, claims);
            if (int.TryParse(id, out int guestId))
                return guestId;

            return -1;
        }

        internal static string GetFromClaims(string type, IEnumerable<Claim> claims)
        {
            return claims.FirstOrDefault(c => c.Type == type)?.Value;
        }

    }
}
