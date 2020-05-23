using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayerApi.Models.Dtos.V10.UserManagement
{
    public class AuthenticateResult
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public ERoles[] Roles { get; set; }
    }
}
