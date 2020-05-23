using DataLayerApi.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DataLayerApi.Services
{
    public static class TokenExtension
    {
        public static int GetUserId(this JwtSecurityToken token)
        {
            var userId = token.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sid);
            return int.Parse(userId.Value);
        }

        public static int GetUserId(this ITokenService tokenService)
        {
            var currentToken = tokenService.GetCurrentToken();
            return currentToken.GetUserId();
        }

        public static string GetUserName(this JwtSecurityToken token)
        {
            var userId = token.Claims.First(c => c.Type == JwtRegisteredClaimNames.UniqueName);
            return userId.Value;
        }

        public static string GetUserName(this ITokenService tokenService)
        {
            var currentToken = tokenService.GetCurrentToken();
            return currentToken.GetUserName();
        }
    }
}
