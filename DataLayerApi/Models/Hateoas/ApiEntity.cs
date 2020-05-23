using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayerApi.Models.Hateoas
{
    public abstract class ApiEntity
    {
        /// <summary>
        /// Odkazy
        /// </summary>
        [Required]
        public List<Link> Links { get; set; }
    }
}
