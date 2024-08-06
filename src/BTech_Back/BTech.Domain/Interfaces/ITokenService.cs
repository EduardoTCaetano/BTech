using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BlitzTech.Domain.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(IdentityUser user, IList<string> roles);
    }
}
