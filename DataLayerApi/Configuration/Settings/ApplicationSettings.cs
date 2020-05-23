using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayerApi.Configuration.Settings
{
    /// <summary>
    /// Nastavení aplikace
    /// </summary>
    public class ApplicationSettings
    {
        /// <summary>
        /// Migrace DB při startu
        /// </summary>
        public bool MigrateDbOnStart { get; set; }

        /// <summary>
        /// Url k frontend
        /// </summary>
        public string FrontEndUrl { get; set; }

        public string[] UrlCors { get; set; } = Array.Empty<string>();

        [JsonProperty("jwt")]
        public JwtSettings Jwt { get; set; }
    }
}
