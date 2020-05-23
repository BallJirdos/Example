using DataLayerApi.Models.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DataLayerApi.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationContext context;
        private readonly ILogger<UnitOfWork> logger;
        private readonly IServiceProvider serviceProvider;
        private bool disposed = false;

        public UnitOfWork(ApplicationContext context, ILogger<UnitOfWork> logger, IServiceProvider serviceProvider)
        {
            this.context = context;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        public bool Autocommit { get; set; }

        public IRepository<E> GetRepository<E>() where E : class, IEntity
        {
            this.logger.LogDebug($"Získávám repository typu {typeof(IRepository<E>)}");
            return new Repository<E>(this.context, this.serviceProvider.GetRequiredService<ILogger<IRepository<E>>>());
        }

        public bool HasTransaction => this.Transaction != null;

        public void BeginTransaction()
        {
            if (!this.HasTransaction)
            {
                this.logger.LogInformation($"Zahajuji transakci");
                this.context.Database.BeginTransaction();
            }
        }

        public void Save()
        {
            this.logger.LogDebug($"Ukládám změny do DB");
            this.context.SaveChanges();
            if (this.HasTransaction)
            {
                this.logger.LogInformation($"Provádím commit");
                this.Transaction.Commit();
            }
        }

        public void Rollback()
        {
            if (this.HasTransaction)
            {
                this.logger.LogInformation($"Provádím rollback");
                this.Transaction.Rollback();
            }
        }

        private IDbContextTransaction Transaction => this.context.Database.CurrentTransaction;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (this.HasTransaction)
                {
                    if (this.Autocommit)
                        this.Save();
                    else
                        this.Rollback();
                }

                if (disposing)
                {
                    this.logger.LogInformation($"Uklízím UOW GC");
                    this.context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
