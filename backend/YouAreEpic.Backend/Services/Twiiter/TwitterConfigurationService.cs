using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouAreEpic.Backend.Services
{
    public class TwitterConfigurationService
    {
        private readonly IConfiguration _configuration;

        public TwitterConfigurationService(IConfiguration configuration, ILogger<TwitterConfigurationService> logger)
        {
            _configuration = configuration;

            if (string.IsNullOrWhiteSpace(ClientId)) logger.LogCritical("ClientId configuration is empty");
            if (string.IsNullOrWhiteSpace(ClientSecret)) logger.LogCritical("ClientSecret configuration is empty");
          
            if (string.IsNullOrWhiteSpace(RedirectEndpoint)) logger.LogCritical("RedirectEndpoint configuration is empty");
          
        }

        public string RedirectEndpoint => _configuration["Server:PublicDomain"] + _configuration["Auth:Twitter:RedirectEndpoint"];
        public string ClientId => _configuration["Auth:Twitter:ClientId"];
        public string ClientSecret => _configuration["Auth:Twitter:ClientSecret"];

        public string ConsumerApiKey => _configuration["Auth:Twitter:ConsumerKey"];

        public string ConsumerApiSecret => _configuration["Auth:Twitter:ConsumerSecret"];

        public string Scopes => _configuration["Auth:Twitter:Scopes"];
    }
}
