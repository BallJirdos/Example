using DataLayerApi.Models.Attributes;
using DataLayerApi.Models.Hateoas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DataLayerApi.Services.Data
{
    public class LinkBuilder<T>
    {
        private const string PARAMETER = "@parameter@";

        private readonly PropertyInfo parameterProperty;
        private readonly string urlReference;

        public LinkBuilder(string urlReference)
        {
            var type = typeof(T);
            var urlParameters = type.GetProperties().Where(p => p.GetCustomAttributes(true).Any(a => a is LinkParameterAttribute));

            if (!urlParameters.Any())
                throw new ArgumentException($"In type {type} not found property with attribute \"LinkParameter\".");

            if (urlParameters.Count() > 1)
                throw new ArgumentException($"In type {type} property with attribute \"LinkParameter\" was found more than once.");

            this.parameterProperty = urlParameters.Single();

            if (!urlReference.Contains(PARAMETER))
                throw new ArgumentException($"Value doesn't has contains {PARAMETER}.", nameof(urlReference));

            if (string.IsNullOrEmpty(urlReference?.Trim()))
                throw new ArgumentException($"Value is empty.", nameof(urlReference));

            this.urlReference = urlReference;
        }

        /// <summary>
        /// Vytvořit link
        /// </summary>
        /// <param name="model">Model ze kterého vychází link</param>
        /// <returns>Link k danému modelu</returns>
        public Link Build(T model)
        {
            var parameterValue = parameterProperty.GetValue(model).ToString();
            var href = this.urlReference.Replace(PARAMETER, parameterValue);
            return new Link
            {
                Rel = parameterValue,
                Href = href
            };
        }
    }
}
