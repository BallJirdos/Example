using DataLayerApi.Models.Entities;
using DataLayerApi.Models.Entities.Core;
using DataLayerApi.Models.Hateoas;
using DataLayerApi.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayerApi.Extensions
{
    public static class HateoasExtension
    {
        public static ApiCollection<E> ToApiCollection<E, EF>(this IEnumerable<EF> source, Func<EF, E> selector, string url)
            where E : class, IEntity
            where EF : class, IEntity
        {
            return source.Select(selector).ToApiCollection(url);
        }

        public static ApiCollection<E> ToApiCollection<E>(this IEnumerable<E> source, string url)
            where E : class, IEntity
        {
            return new ApiCollectionBuilder<E>(url).Build(source);
        }

        public static Link ToLink<E>(this E source, string url)
            where E : class, IEntity
        {
            return new LinkBuilder<E>(url).Build(source);
        }

        public static Link ToEnumItemLink(this EnumItem source)
        {
            return source.ToLink($"api/core/enumType/{source.EnumItemType.NormalizedName}/item/@parameter@");
        }

        public static Link ToEnumItemTypeLink(this EnumItemType source)
        {
            return source.ToLink($"api/core/enumType/@parameter@");
        }

        public static ApiCollection<EnumItem> ToEnumItemApiCollection(this EnumItemType source)
        {
            return source.EnumItems.ToApiCollection($"api/core/enumType/{source.NormalizedName}/item/@parameter@");
        }
    }
}
