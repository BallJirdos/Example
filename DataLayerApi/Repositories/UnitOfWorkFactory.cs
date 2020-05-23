using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayerApi.Repositories
{
    public class UnitOfWorkFactory
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<UnitOfWorkFactory> logger;

        public UnitOfWorkFactory(IServiceProvider serviceProvider, ILogger<UnitOfWorkFactory> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }

        public UnitOfWork Create()
        {
            this.logger.LogDebug($"Získávám UnitOfWork");
            return this.serviceProvider.GetRequiredService<UnitOfWork>();
        }
    }
}
