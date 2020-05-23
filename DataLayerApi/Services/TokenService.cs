using DataLayerApi.Configuration.Settings;
using DataLayerApi.Models.Entities.UserManagement;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DataLayerApi.Services
{
    public class TokenService : ITokenService
    {
        private const string AUTH_HEADER = "Authorization";
        private const string TOKEN_PREFIX = "Bearer";

        private readonly JwtSettings jwtSettings;
        private readonly IHttpContextService contextService;

        public TokenService(IOptions<ApplicationSettings> options, IHttpContextService contextService)
        {
            this.jwtSettings = options.Value.Jwt;
            this.contextService = contextService;
        }

        /// <summary>
        /// Vytvořit token
        /// </summary>
        /// <param name="user">Pro uživatele</param>
        /// <returns>Vygenerovaný token</returns>
        public JwtSecurityToken CreateToken(User user)
        {
            var authClaims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtSettings.SecureKey));

            return new JwtSecurityToken(
                issuer: this.jwtSettings.ValidIssuer,
                audience: this.jwtSettings.ValidAudience,
                expires: DateTime.UtcNow + this.jwtSettings.Expiration,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
        }

        public bool HasToken()
        {
            return this.contextService.HasHeader(AUTH_HEADER)
                && this.contextService.GetHeaderValue(AUTH_HEADER).StartsWith($"{TOKEN_PREFIX} ");
        }

        public JwtSecurityToken GetCurrentToken()
        {
            var authHeader = this.contextService.GetHeaderValue(AUTH_HEADER);
            if (!authHeader.StartsWith($"{TOKEN_PREFIX} "))
                throw new SecurityTokenException("Token in header not found");

            var plainToken = authHeader.Substring(TOKEN_PREFIX.Length + 1);

            return this.ReadToken(plainToken);
        }

        /// <summary>
        /// Číst token z řetězce
        /// </summary>
        /// <param name="jwtInput"></param>
        /// <returns></returns>
        private JwtSecurityToken ReadToken(string jwtInput)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            // Check Token Format
            if (!jwtHandler.CanReadToken(jwtInput))
                throw new Exception("The token doesn't seem to be in a proper JWT format.");

            return jwtHandler.ReadJwtToken(jwtInput);

        }
    }
}
