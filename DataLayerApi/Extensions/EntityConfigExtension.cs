using DataLayerApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DataLayerApi.Extensions
{
    public static class EntityConfigExtension
    {
        /// <summary>
        /// Registrovat všechny objekty implementující rozhraní IEntity
        /// </summary>
        public static void RegisterDbSet(this ModelBuilder modelBuilder)
        {
            Type entityType = typeof(IEntity);
            foreach (Type type in AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic).SelectMany(a => a.ExportedTypes))
            {
                if (type.IsClass && entityType.IsAssignableFrom(type))
                {
                    modelBuilder.Entity(type);
                }
            }
        }

        public static void CreateSet(this DbContext context)
        {
            Type entityType = typeof(IEntity);
            foreach (Type type in AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic).SelectMany(a => a.ExportedTypes))
            {
                if (type.IsClass && entityType.IsAssignableFrom(type))
                {
                    var mi = context.GetType().GetMethod("Set");
                    var fooRef = mi.MakeGenericMethod(type);
                    //yield return (DbSet<object>)
                    fooRef.Invoke(context, null);
                }
            }
        }

        /// <summary>
        /// Spustit config pro všechny objekty implementující rozhraní IEntity
        /// </summary>
        public static void ConfigureEntity(this ModelBuilder modelBuilder)
        {
            Type entityType = typeof(IEntity);
            var currentAssembly = Assembly.GetExecutingAssembly();

            bool configExpr(Type t) => t.GetInterfaces()
            .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)
            && i.GenericTypeArguments.Any(e => entityType.IsAssignableFrom(e)));

            modelBuilder.ApplyConfigurationsFromAssembly(currentAssembly, configExpr);
        }

        public static IndexBuilder<E> HasDefaultUniqueIndex<E>(this EntityTypeBuilder<E> builder, Expression<Func<E, object>> indexExpression = null)
            where E : class, IEntityEnumItem
        {
            Expression<Func<E, object>> codeIndexExpression = (p) => new { p.NormalizedName };
            var expr = indexExpression ?? codeIndexExpression;

            return builder.HasUniqueIndex(expr);
        }

        public static IndexBuilder<E> HasUniqueIndex<E>(this EntityTypeBuilder<E> builder, Expression<Func<E, object>> indexExpression)
            where E : class, IEntity
        {
            return builder.HasBasicIndex(indexExpression).IsUnique();
        }

        public static IndexBuilder<E> HasBasicIndex<E>(this EntityTypeBuilder<E> builder, Expression<Func<E, object>> indexExpression)
            where E : class, IEntity
        {
            var entityType = typeof(E);
            var entityName = entityType.Name;

            var indexColumns = GetMembersName(indexExpression);
            var indexColumnsJoined = string.Join("_", indexColumns);

            var indexName = $"IX_{entityName}_{indexColumnsJoined}_Unique";

            return builder.HasIndex(indexExpression).HasName(indexName);
        }

        private static string[] GetMembersName<E>(Expression<Func<E, object>> expression) where E : class, IEntity
        {
            return (expression.Body as NewExpression)?.Members.Select(m => m.Name).ToArray();
        }
    }
}
