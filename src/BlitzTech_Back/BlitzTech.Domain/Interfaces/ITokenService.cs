

using BlitzTech.Data.Context;

namespace BlitzTech.Domain.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(UnitOfWork user);
    }
}
