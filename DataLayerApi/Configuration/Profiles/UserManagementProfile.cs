using AutoMapper;
using DataLayerApi.Extensions;
using DataLayerApi.Models.Dtos.V10.UserManagement;
using DataLayerApi.Models.Entities.UserManagement;

namespace DataLayerApi.Configuration.Profiles
{
    public class UserManagementProfile : Profile
    {
        public UserManagementProfile()

        {
            this.CreateMap<Role, RoleModel>()
                //.ForMember(gm => gm.Users, opts => opts.MapFrom(gm => gm.UserRoles.ToApiCollection(gp => gp.User, "api/usermanagement/user/@parameter@")))
                .ReverseMap();

            this.CreateMap<User, UserModel>()
                //.ForMember(um => um.Roles, opts => opts.MapFrom(gm => gm.UserRoles.ToApiCollection(gp => gp.Role, "api/usermanagement/group/@parameter@")))
                //.ForMember(um => um.Accounts, opts => opts.MapFrom(gm => gm.Logins.ToApiCollection("api/usermanagement/account/@parameter@")))
                .ReverseMap()
                .ForMember(u=>u.NormalizedUserName, opts=>opts.MapFrom(um=>um.UserName.ToUpper()));

            this.CreateMap<UserRegistrationModel, User>()
                .ForMember(um => um.UserName, opts => opts.MapFrom(gm => gm.Email));

            this.CreateMap<Login, AccountModel>()
                .ForMember(am => am.User, opts => opts.MapFrom(a => a.User.ToLink("api/usermanagement/user/@parameter@")))
                .ForMember(am => am.AccountType, opts => opts.MapFrom(a => a.AccountType.ToEnumItemLink()))
                .ReverseMap()
                .ForMember(a => a.User, opts => opts.Ignore())
                .ForMember(a => a.AccountType, opts => opts.Ignore())
                .ForMember(a => a.UserId, opts => opts.MapFrom(am => am.User.Rel))
                .ForMember(a => a.EnumItemId_AccountType, opts => opts.MapFrom(am => am.AccountType.Rel));
        }
    }
}
