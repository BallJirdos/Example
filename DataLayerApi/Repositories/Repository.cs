using DataLayerApi.Models.Entities;
using DataLayerApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DataLayerApi.Repositories
{
    public class Repository<E> : IRepository<E> where E : class, IEntity
    {
        private readonly ApplicationContext context;
        protected readonly ILogger<IRepository<E>> logger;

        public Repository(ApplicationContext context, ILogger<IRepository<E>> logger)
        {
            using var startWatch = new StopWatch(logger, LogLevel.Debug, $"Initialize Repository for entity {typeof(E).Name}");
            this.logger = logger;
            this.context = context;
            this.DbSet = context.Set<E>();
        }

        public DbSet<E> DbSet { get; }

        public virtual IQueryable<E> Get(Expression<Func<E, bool>> predicate = null)
        {
            using var startWatch = new StopWatch(this.logger, LogLevel.Debug, $"Call Get method for Repository for entity {typeof(E).Name}");
            return predicate == null ? this.DbSet : this.DbSet.Where(predicate);
        }

        public virtual E GetById(int id)
        {
            this.logger.LogDebug($"Vracím entitu dle ID {id}");
            return this.DbSet.FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Existuje entita
        /// </summary>
        /// <param name="id">ID entity</param>
        /// <returns>Exituje/neexistuje</returns>
        public virtual bool EntityExists(int id)
        {
            this.logger.LogDebug($"Zjistuji existenci ID: {id}.");
            return this.DbSet.Any(e => e.Id == id);
        }

        public virtual int Insert(E obj)
        {
            var result = this.DbSet.Add(obj);
            this.context.SaveChanges();
            return result.Entity.Id;
        }

        public virtual void Update(E obj)
        {
            this.logger.LogDebug($"Updatuji entitu s ID {obj.Id}");
            this.context.DetachLocal(obj, obj.Id);
            this.context.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            this.Delete(e => e.Id == id);
        }

        public void Delete(Expression<Func<E, bool>> predicate)
        {
            this.logger.LogDebug($"Mažu entity dle podmínky {predicate}");
            var existingItems = this.Get(predicate);
            foreach (E item in existingItems)
            {
                this.DbSet.Remove(item);
            }
            this.context.SaveChanges();
        }
    }
}
