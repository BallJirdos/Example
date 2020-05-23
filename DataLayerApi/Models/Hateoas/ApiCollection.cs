using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DataLayerApi.Models.Hateoas
{
    /// <summary>
    /// Kolekce DTO
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiCollection<T> : ApiEntity
    {
        /// <summary>
        /// Raw data
        /// </summary>
        [JsonIgnore]
        public List<T> Data { get; set; }
        
        /// <summary>
        /// Celkem položek
        /// </summary>
        public int? TotalItems { get; set; }

        [JsonIgnore]
        public int? TotalPages { get; set; }
    }
}
