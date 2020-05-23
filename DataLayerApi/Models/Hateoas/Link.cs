using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayerApi.Models.Hateoas
{
    /// <summary>
    /// Odkaz
    /// </summary>
    public class Link
    {
        /// <summary>
        /// Url odkaz
        /// </summary>
        [Required]
        public string Href { get; set; }

        /// <summary>
        /// Hodnota
        /// </summary>
        [Required]
        public string Rel { get; set; }
    }
}
