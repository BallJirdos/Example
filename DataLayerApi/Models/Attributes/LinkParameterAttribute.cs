using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayerApi.Models.Attributes
{
    /// <summary>
    /// Parametr pro URL
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class LinkParameterAttribute : Attribute
    {
    }
}
