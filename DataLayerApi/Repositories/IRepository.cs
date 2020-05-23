using DataLayerApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataLayerApi.Repositories
{
    public interface IRepository<E> where E : class, IEntity
    {
        /// <summary>
        /// DbSet pro entitu
        /// </summary>
        DbSet<E> DbSet { get; }

        /// <summary>
        /// Získat záznamy
        /// </summary>
        /// <param name="predicate">Podmínka, pokud není zadána, vrátí všechny záznamy</param>
        /// <returns>Kolekce entit</returns>
        IQueryable<E> Get(Expression<Func<E, bool>> predicate = null);

        /// <summary>
        /// Existuje entita
        /// </summary>
        /// <param name="id">ID entity</param>
        /// <returns>Exituje/neexistuje</returns>
        bool EntityExists(int id);

        /// <summary>
        /// Získat záznam dle ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        E GetById(int id);

        /// <summary>
        /// Vložit záznam
        /// </summary>
        /// <param name="obj">Entita</param>
        /// <returns>ID záznamu</returns>
        int Insert(E obj);

        /// <summary>
        /// Upravit záznam
        /// </summary>
        /// <param name="obj">Entita</param>
        void Update(E obj);

        /// <summary>
        /// Smazat záznam dle ID
        /// </summary>
        /// <param name="id">ID záznamu</param>
        void Delete(int id);

        /// <summary>
        /// Smazat záznam/y dle definované podmínky
        /// </summary>
        /// <param name="predicate">Podmínka pro smazání</param>
        void Delete(Expression<Func<E, bool>> predicate);
    }
}