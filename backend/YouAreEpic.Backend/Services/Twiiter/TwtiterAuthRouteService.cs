using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YouAreEpic.Backend.Services
{
    public class TwtiterAuthRouteService
    {
        private readonly TwitterConfigurationService _configService;

        public TwtiterAuthRouteService(TwitterConfigurationService configService)
        {
            _configService = configService;
        }

        
        public string RedirectUri => _configService.RedirectEndpoint;

        public Uri BuildAuthUri(string codeVerifier, string state)
        {
          
            string code_challenge;

            using (var sha256 = SHA256.Create())
            {
                var challengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
                code_challenge = Base64Url.Encode(challengeBytes);
            }

            var query = new QueryBuilder
            {
                { "response_type", "code" },
                { "client_id", _configService.ClientId },
                { "redirect_uri", RedirectUri },
                { "scope", _configService.Scopes },
                { "state", state },
                { "code_challenge", code_challenge },
                { "code_challenge_method", "S256" }
            };

            var uriBuilder = new UriBuilder
            {
                Scheme = "https",
                Host = "twitter.com",
                Path = "i/oauth2/authorize",
                Query = query.ToQueryString().ToUriComponent(),
                Port = 443
            };

            return uriBuilder.Uri;
        }
    }
}
