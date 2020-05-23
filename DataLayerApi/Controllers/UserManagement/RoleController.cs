using AutoMapper;
using DataLayerApi.Models.Dtos.V10.UserManagement;
using DataLayerApi.Models.Entities.UserManagement;
using DataLayerApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace DataLayerApi.Controllers.UserManagement
{
    [Authorize()]
    [Route("api/usermanagement/[controller]")]
    public class RoleController : EnumItemControllerBase<Role, RoleModel>
    {
        private readonly UserManager<User> userManager;

        public RoleController(IMapper mapper,
            IRepository<Role> repository,
            ILogger<RoleController> logger,
            UserManager<User> userManager)
            : base(mapper, repository, logger)
        {
            this.userManager = userManager;
        }

        protected override IQueryable<Role> GetData(Expression<Func<Role, bool>> predicate = null)
        {
            return base.GetData();
        }

        /// <summary>
        /// Přidat uživatele do skupiny
        /// </summary>
        /// <param name="roleCode">Kód skupiny</param>
        /// <param name="userName">Uživatelské jméno</param>
        [HttpPost("{roleCode}/userAdd/{userName}")]
        public ActionResult AddUser(ERoles roleCode, string userName)
        {
            var user = this.userManager.FindByName(userName);
            this.userManager.AddToRole(user, roleCode);
            return this.Ok();
        }

        /// <summary>
        /// Odebrat uživatele ze skupiny
        /// </summary>
        /// <param name="roleCode">Kód skupiny</param>
        /// <param name="userName">Uživatelské jméno</param>
        [HttpDelete("{roleCode}/userRemove/{userName}")]
        public ActionResult RemoveUser(ERoles roleCode, string userName)
        {
            var user = this.userManager.FindByName(userName);
            this.userManager.RemoveFromRole(user, roleCode);
            return this.Ok();
        }

        [NonAction]
        public override ActionResult Delete(int id)
        {
            return base.Delete(id);
        }

        [NonAction]
        public override ActionResult DeleteByCode([Required] string code)
        {
            return base.DeleteByCode(code);
        }

        [NonAction]
        public override ActionResult<int> Create(RoleModel item)
        {
            return base.Create(item);
        }

        [NonAction]
        public override ActionResult Update(RoleModel item)
        {
            return base.Update(item);
        }
    }
}
