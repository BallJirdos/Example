using DataLayerApi.Extensions;
using DataLayerApi.Models.Entities.UserManagement;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLayerApi.Models.Entities
{
    public class ApplicationContext : IdentityDbContext<User, Role, int>
    {
        //private readonly ILogger<ApplicationContext> logger;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //logger = LoggerFactory.Create();
            //using (var startWatch = new StopWatch(logger, LogLevel.Debug, $"Initialize- constructor {typeof(ApplicationContext).Name}")) ;
            //this.logger = logger;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //using (var startWatch = new StopWatch(this.logger, LogLevel.Debug, $"Initialize- OnModelCreating {typeof(ApplicationContext).Name}")) ;
            modelBuilder.RegisterDbSet();
            modelBuilder.ConfigureEntity();

            base.OnModelCreating(modelBuilder);
        }
    }
}
