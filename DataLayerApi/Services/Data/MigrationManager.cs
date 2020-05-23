using DataLayerApi.Configuration.Settings;
using DataLayerApi.Models.Dtos.V10.UserManagement;
using DataLayerApi.Models.Entities;
using DataLayerApi.Models.Entities.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayerApi.Services.Data
{
    public static class MigrationManager
    {
        /// <summary>
        /// Migrovat DB při startu
        /// </summary>
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var servProvider = scope.ServiceProvider;

                //var logger = servProvider.GetRequiredService<ILogger>();

                var optionsSettings = servProvider.GetRequiredService<IOptions<ApplicationSettings>>();
                var settings = optionsSettings.Value;

                using (var appContext = servProvider.GetRequiredService<ApplicationContext>())
                {
                    try
                    {
                        if (settings.MigrateDbOnStart)
                            appContext.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        //logger.LogError($"Chyba migrace: {ex}");
                        throw;
                    }
                }

                var userManager = servProvider.GetRequiredService<UserManager<User>>();
                if (!userManager.Users.Any())
                {
                    var user = new User
                    {
                        Email = "admin@admin.com",
                        UserName = "admin@admin.com",
                        FirstName = "Admin",
                        LastName = "Adminovic",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        EmailConfirmed = true
                    };

                    var result = Task.Run(() => userManager.CreateAsync(user, "adminadmin")).Result;
                    userManager.AddToRole(user, ERoles.Admin);
                    userManager.AddToRole(user, ERoles.Guest);
                }
            }

            return host;
        }
    }
}
