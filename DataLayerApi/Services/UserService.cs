using AutoMapper;
using DataLayerApi.Models.Dtos.V10.Core;
using DataLayerApi.Models.Dtos.V10.UserManagement;
using DataLayerApi.Models.Entities;
using DataLayerApi.Models.Entities.UserManagement;
using DataLayerApi.Repositories;
using DataLayerApi.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IdentityModel.Tokens.Jwt;

namespace DataLayerApi.Services
{
    public class UserService : IUserService
    {
        private int[] cachedRoles;

        private readonly IMapper mapper;
        private readonly ITokenService tokenService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IRepository<User> userRepository;
        private readonly DbSet<IdentityUserRole<int>> userRoleRepository;

        public UserService(IMapper mapper,
            ApplicationContext applicationContext,
            ITokenService tokenService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IRepository<User> userRepository)
        {
            this.mapper = mapper;
            this.tokenService = tokenService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userRepository = userRepository;
            this.userRoleRepository = applicationContext.Set<IdentityUserRole<int>>();
            var userId = tokenService.HasToken() ? tokenService.GetUserId() : (int?)null;
            if (userId.HasValue)
            {
                this.User = userRepository.GetById(userId.Value);
            }
        }

        /// <summary>
        /// Overeni uzivatele
        /// </summary>
        /// <param name="username">Uzivatelske jmeno</param>
        /// <param name="password">Heslo</param>
        /// <returns>Uzivatel</returns>
        public AuthenticateResult Authenticate(string username, string password)
        {
            var user = this.userManager.FindByNameAsync(username).Result;
            //if (user != null && this.userManager.CheckPasswordAsync(user, password).Result)
            if (user != null && this.signInManager.PasswordSignInAsync(user, password, false, false).Result.Succeeded)
            {
                return this.GenerateToken(user);
            }
            return null;
        }

        /// <summary>
        /// Odhlásit uživatele
        /// </summary>
        public void SignOut()
        {
            this.signInManager.SignOutAsync().Wait();
        }

        /// <summary>
        /// Aktualizovat token
        /// </summary>
        /// <returns></returns>
        private AuthenticateResult GenerateToken(User user = null)
        {
            user ??= this.User;

            var token = this.tokenService.CreateToken(user);

            var roles = this.GetRoles(user.Id).Select(r => r.ToEntityItemEnum<ERoles>()).ToArray();
            return new AuthenticateResult
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Roles = roles
            };
        }

        /// <summary>
        /// Aktualizovat token
        /// </summary>
        /// <returns></returns>
        public AuthenticateResult RefreshToken()
        {
            return this.GenerateToken();
        }

        public int? UserId => this.User?.Id;

        public string UserName => this.User?.UserName;

        public User User { get; }

        public int CreateUser(UserRegistrationModel userModel)
        {
            var user = this.mapper.Map<User>(userModel);
#if DEBUG
            var userFind = this.userManager.FindByEmail(user.Email);
            user = userFind ?? user;
#endif

            this.userManager.Create(user, userModel.Password);
            this.userManager.AddToRole(user, ERoles.Guest);

            var registerToken = this.userManager.GenerateEmailConfirmationToken(user);

            return user.Id;
        }

        /// <summary>
        /// Zapomenuté heslo
        /// </summary>
        /// <param name="model">Model pro zapomenuté heslo</param>
        public void ForgotPassword(ForgotPasswordModel model)
        {
            var user = this.userManager.FindByEmail(model.Email);
            if (user == null)
                throw new HttpResponseException("User not found", HttpStatusCode.BadRequest);

            var token = this.userManager.GeneratePasswordResetToken(user);
        }

        /// <summary>
        /// Reset hesla
        /// </summary>
        /// <param name="model">Model pro reset hesla</param>
        public void ResetPassword(ResetPasswordModel model)
        {
            var user = this.userManager.FindByEmail(model.Email);
            if (user == null)
                throw new HttpResponseException("User not found", HttpStatusCode.BadRequest);

            this.userManager.ResetPasswordAsync(user, model.Token, model.Password);
        }

        /// <summary>
        /// Získat ID uživatele
        /// </summary>
        /// <returns></returns>
        public int GetUserId()
        {
            return this.UserId ?? throw new HttpResponseException("User isn't authorized.", HttpStatusCode.Unauthorized);
        }

        /// <summary>
        /// Získat nacachované role (pro aktuálního uživatele)
        /// </summary>
        /// <returns>Role</returns>
        public IEnumerable<int> GetCachedRoles()
        {
            return this.cachedRoles ?? (this.cachedRoles = this.GetRoles().ToArray());
        }

        /// <summary>
        /// Získat skupiny uživatele
        /// </summary>
        /// <param name="userId">ID uživatele, pro kterého chceme zobrazit práva (nepovinné, pokud je null, zobrazíme práva aktuálího uživatele)</param>
        /// <returns>Dostupné skupiny</returns>
        public IQueryable<int> GetRoles(int? userId = null)
        {
            if (!this.UserId.HasValue && !userId.HasValue)
                return new[] { ERoles.Guest.ToEntityId() }.AsQueryable();

            return this.userRepository.Get()
                .Join(this.userRoleRepository, u => u.Id, ur => ur.UserId, (u, ur) => new { ur.UserId, Role = ur.RoleId })
                .Where(u => u.UserId == (userId ?? this.UserId))
                .OrderBy(r => r.Role)
                .Select(r => r.Role);
        }

        /// <summary>
        /// Získat skupiny uživatele
        /// </summary>
        /// <param name="userName">Uživatelské jméno uživatele, pro kterého chceme zobrazit práva (nepovinné, pokud je null, zobrazíme práva aktuálího uživatele)</param>
        /// <returns>Dostupné skupiny</returns>
        public IQueryable<int> GetRoles(string userName)
        {
            if (!this.UserId.HasValue && string.IsNullOrEmpty(userName))
                return new[] { ERoles.Guest.ToEntityId() }.AsQueryable();

            return this.userRepository.Get()
                .Join(this.userRoleRepository, u => u.Id, ur => ur.UserId, (u, ur) => new { u.UserName, Role = ur.RoleId })
                .Where(u => u.UserName == (userName ?? this.UserName))
                .OrderBy(r => r.Role)
                .Select(r => r.Role);
        }
    }
}
