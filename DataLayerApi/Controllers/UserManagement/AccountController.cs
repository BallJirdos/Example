using AutoMapper;
using DataLayerApi.Models.Dtos.V10.UserManagement;
using DataLayerApi.Models.Entities.UserManagement;
using DataLayerApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DataLayerApi.Controllers.UserManagement
{
    [NonController]
    [Route("api/usermanagement/[controller]")]
    public class AccountController : EntityControllerBase<Login, AccountModel>
    {
        public AccountController(IMapper mapper, IRepository<Login> repository, ILogger<AccountController> logger)
            : base(mapper, repository, logger)
        {
        }
    }
}
