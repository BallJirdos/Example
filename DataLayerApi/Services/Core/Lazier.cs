using Microsoft.Extensions.DependencyInjection;
using System;

namespace DataLayerApi.Services.Core
{
    /// <summary>
    /// Implementace Lazy load pro DI
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Lazier<T> : Lazy<T> where T : class
    {
        /// <summary>
        /// Constructor obsahujici resolve pozadovaneho typu
        /// </summary>
        /// <param name="provider"></param>
        public Lazier(IServiceProvider provider)
            : base(() => provider.GetRequiredService<T>())
        {
        }
    }
}
