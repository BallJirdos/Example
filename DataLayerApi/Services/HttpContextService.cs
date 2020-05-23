using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayerApi.Services
{
    public class HttpContextService : IHttpContextService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public HttpContextService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        private HttpContext HttpContext => this.httpContextAccessor.HttpContext;

        public bool HasHeader(string headerName)
        {
            return this.HttpContext.Request.Headers.ContainsKey(headerName);
        }

        public string GetHeaderValue(string headerName)
        {
            if (this.HasHeader(headerName))
                return this.HttpContext.Request.Headers[headerName];

            throw new KeyNotFoundException($"Request neobsahuje Header s klíčem {headerName}");
        }
    }
}
