using DataLayerApi.Exceptions;
using DataLayerApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;

namespace DataLayerApi.Repositories
{
    public static class RepositoryExtension
    {
        /// <summary>
        /// Update položky nastavením IsEnabled na false
        /// </summary>
        /// <typeparam name="E">Číselníková položka</typeparam>
        /// <param name="repository">Repository s číselníkovou položkou</param>
        /// <param name="predicate">Podmínka pro nalezení položky, výsledek musí nalézt jediný záznam</param>
        public static void DeleteItem<E>(this IRepository<E> repository, Expression<Func<E, bool>> predicate) where E : class, IEntityEnumItem
        {
            var item = repository.Get().Single(predicate);
            item.IsEnabled = false;
            repository.Update(item);
        }

        public static E GetByCode<E>(this IRepository<E> repository, string code) where E : class, IEntityEnumItem
        {
            return repository.Get(e => e.NormalizedName == code).SingleOrDefault();
        }

        public static bool IsEntityExists<E>(this IRepository<E> repository, int id) where E : class, IEntity
        {
            return repository.GetById(id) != null;
        }

        /// <summary>
        /// Kontrola exitence ID
        /// </summary>
        /// <typeparam name="E">Dana tabulka</typeparam>
        /// <param name="repository">Repository obsluhující tabulku</param>
        /// <param name="Id">ID pro kontrolu</param>
        public static void CheckIdExists<E>(this IRepository<E> repository, int Id) where E : class, IEntity
        {
            if (!repository.EntityExists(Id))
            {
                throw new HttpResponseException($"ID for repostiory {typeof(E).Name} not exists.", HttpStatusCode.BadRequest);
            }
        }

        public static void DetachLocal<T>(this ApplicationContext context, T t, int entryId)
            where T : class, IEntity
        {
            try
            {
                var local = context.Set<T>()
                                .Local
                                .FirstOrDefault(entry => entry.Id.Equals(entryId));
                if (local != null)
                {
                    context.Entry(local).State = EntityState.Detached;
                }
            }
            catch (InvalidOperationException)
            {

            }
            finally
            {
                context.Entry(t).State = EntityState.Modified;
            }

        }
    }
}
