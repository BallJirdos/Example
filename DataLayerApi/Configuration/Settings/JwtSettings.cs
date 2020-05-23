using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataLayerApi.Configuration.Settings
{
    public class JwtSettings
    {
        public string SecureKey { get; set; }

        [JsonPropertyName("expiration")]
        public string ExpirationStr
        {
            get { return this.Expiration.ToString(); }
            set { this.Expiration = TimeSpan.Parse(value); }
        }

        [JsonIgnore]
        public TimeSpan Expiration { get; set; }

        public string ValidAudience { get; set; }

        public string ValidIssuer { get; set; }

        public bool RequireHttpsMetadata { get; set; }
    }
}
