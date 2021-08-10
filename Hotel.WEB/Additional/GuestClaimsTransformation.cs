using Hotel.DAL.EF;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using static Hotel.DAL.EF.GuestIdentity;
using Hotel.WEB.Additional;

namespace Hotel.WEB.Cookie
{
    public class GuestClaimsTransformation : IClaimsTransformation
    {
        private readonly UserManager<GuestIdentity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public GuestClaimsTransformation(UserManager<GuestIdentity> userService,
           RoleManager<IdentityRole> roleManager)
        {
            _userManager = userService;
            _roleManager = roleManager;
        }


        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var clone = principal.Clone();
            var newIdentity = (ClaimsIdentity)clone.Identity;

            var userId = User.GetUserIdFromClaims(principal.Claims);
            if (userId == null)
                return principal;

            var user =  await _userManager.FindByIdAsync(userId);
            if (user == null)
                return principal;

            var claim = new Claim(ClaimType.GuestId, user.GuestID.ToString());
            newIdentity.AddClaim(claim);

            //foreach (var role in await _userManager.GetRolesAsync(user))
            //{
            //    claim = new Claim(ClaimType.Role, role);
            //    newIdentity.AddClaim(claim);
            //}

            return clone;
        }
    }
}
