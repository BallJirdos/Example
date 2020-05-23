using DataLayerApi.Models.Hateoas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayerApi.Services.Data
{
    /// <summary>
    /// Vytvoření API kolekce
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiCollectionBuilder<T>
    {
        private readonly LinkBuilder<T> linkBuilder;

        public ApiCollectionBuilder(string urlReference)
        {
            this.linkBuilder = new LinkBuilder<T>(urlReference);
        }

        /// <summary>
        /// Vytvoření API kolekce
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public ApiCollection<T> Build(IEnumerable<T> collection)
        {
            var list = collection?.ToList() ?? new List<T>();
            return new ApiCollection<T>
            {
                Data = list,
                Links = list.Select(i => this.linkBuilder.Build(i)).ToList(),
                TotalItems = list.Count
            };
        }
    }
}
