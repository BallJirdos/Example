using DataLayerApi.Models.Dtos.V10.UserManagement;
using DataLayerApi.Models.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Identity
{
    public static class UserManagerExtension
    {
        public static async Task<IdentityResult> AddToRoleAsync(this UserManager<User> userManager, User user, params ERoles[] roles)
        {
            return await userManager.AddToRolesAsync(user, roles.Select(r => r.ToString()));
        }

        public static IdentityResult Create(this UserManager<User> userManager, User user, string password)
        {
            return Task.Run(() => userManager.CreateAsync(user, password)).Result;
        }

        public static string GenerateEmailConfirmationToken(this UserManager<User> userManager, User user)
        {
            return Task.Run(() => userManager.GenerateEmailConfirmationTokenAsync(user)).Result;
        }

        public static string GeneratePasswordResetToken(this UserManager<User> userManager, User user)
        {
            return Task.Run(() => userManager.GeneratePasswordResetTokenAsync(user)).Result;
        }

        public static User FindByEmail(this UserManager<User> userManager, string email)
        {
            return Task.Run(() => userManager.FindByEmailAsync(email)).Result;
        }

        public static IdentityResult AddToRole(this UserManager<User> userManager, User user, params ERoles[] roles)
        {
            return Task.Run(() => userManager.AddToRoleAsync(user, roles)).Result;
        }

        public static Task<IdentityResult> RemoveFromRoleAsync(this UserManager<User> userManager, User user, params ERoles[] roles)
        {
            return userManager.RemoveFromRolesAsync(user, roles.Select(r => r.ToString()));
        }

        public static IdentityResult RemoveFromRole(this UserManager<User> userManager, User user, params ERoles[] roles)
        {
            return Task.Run(() => userManager.RemoveFromRoleAsync(user, roles)).Result;
        }

        public static User FindByName(this UserManager<User> userManager, string userName)
        {
            return Task.Run(() => userManager.FindByNameAsync(userName)).Result;
        }

        public static User FindById(this UserManager<User> userManager, int userId)
        {
            return Task.Run(() => userManager.FindByIdAsync(userId.ToString())).Result;
        }
    }
}
