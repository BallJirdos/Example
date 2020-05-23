using DataLayerApi.Models.Entities.UserManagement;
using System.IdentityModel.Tokens.Jwt;

namespace DataLayerApi.Services
{
    public interface ITokenService
    {
        JwtSecurityToken CreateToken(User user);
        JwtSecurityToken GetCurrentToken();
        bool HasToken();
    }
}