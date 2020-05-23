using AutoMapper;
using DataLayerApi.Exceptions;
using DataLayerApi.Models.Dtos.V10.Core;
using DataLayerApi.Models.Dtos.V10.UserManagement;
using DataLayerApi.Models.Entities.UserManagement;
using DataLayerApi.Repositories;
using DataLayerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Web;

namespace DataLayerApi.Controllers.UserManagement
{
    [Authorize()]
    [Route("api/usermanagement/[controller]")]
    public class UserController : EntityControllerBase<User, UserModel>
    {
        private readonly IUserService userService;
        private readonly ITokenService tokenService;
        private readonly UserManager<User> userManager;

        public UserController(IMapper mapper,
            IUserService userService,
            ITokenService tokenService,
            IRepository<User> repository,
            ILogger<UserController> logger,
            UserManager<User> userManager)
            : base(mapper, repository, logger)
        {
            this.userService = userService;
            this.tokenService = tokenService;
            this.userManager = userManager;
        }

        /// <summary>
        /// Přidat uživatele
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [NonAction]
        public override ActionResult<int> Create([Required, FromBody] UserModel item)
        {
            return this.Ok(0);
        }

        /// <summary>
        /// Registrovat uživatele
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult<int> Register([Required, FromBody] UserRegistrationModel item)
        {
            var userId = this.userService.CreateUser(item);

            return this.Ok(userId);
        }


        /// <summary>
        /// Verifikovat email uživatele
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="email">Email uživatele</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("verifyAcount")]
        public IActionResult ConfirmEmail(string token, string email)
        {
            var decodeMail = HttpUtility.UrlDecode(email);
            var decodeToken = HttpUtility.UrlDecode(token);
            var user = this.userManager.FindByEmailAsync(decodeMail).Result;
            if (user == null)
                throw new HttpResponseException("User not found", HttpStatusCode.BadRequest);

            var result = this.userManager.ConfirmEmailAsync(user, decodeToken).Result;
            return result.Succeeded ?
                this.Ok() :
                throw new HttpResponseException(string.Join(", ", result.Errors));
        }

        /// <summary>
        /// Ověřit uživatele
        /// </summary>
        /// <param name="model">Model přihlašovacích údajů</param>
        /// <returns>Výsledek</returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ActionResult<AuthenticateResult> Authenticate([FromBody]AuthenticateModel model)
        {
            var user = this.userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return this.BadRequest(new { message = "Username or password is incorrect" });

            return this.Ok(user);
        }

        /// <summary>
        /// Odhlásit uživatele
        /// </summary>
        [AllowAnonymous]
        [HttpPost("signout")]
        public ActionResult SignOut()
        {
            this.userService.SignOut();

            return this.Ok();
        }

        [AllowAnonymous]
        [HttpPost("forgotPwd")]
        public IActionResult ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            this.userService.ForgotPassword(forgotPasswordModel);

            return this.Ok();
        }

        [AllowAnonymous]
        [HttpPost("resetPwd")]
        public IActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            this.userService.ResetPassword(resetPasswordModel);

            return this.Ok();
        }

        /// <summary>
        /// Aktualizovat token
        /// </summary>
        /// <returns>Token</returns>
        [HttpPost("refreshtoken")]
        public ActionResult<AuthenticateResult> RefreshToken()
        {
            var token = this.userService.RefreshToken();
            return this.Ok(token);
        }

        /// <summary>
        /// Role uživatele
        /// </summary>
        /// <param name="userName">Uživatelské jméno</param>
        /// <returns>Role uživatele</returns>
        [HttpPost("{userName}/roles")]
        public ActionResult<List<ERoles>> GetRoles([Required, FromRoute] string userName)
        {
            var roles = userService.GetRoles(userName)
                .AsEnumerable()
                .Select(r => r.ToEntityItemEnum<ERoles>());

            return this.Ok(roles.ToList());
        }

        [NonAction]
        public override ActionResult Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
