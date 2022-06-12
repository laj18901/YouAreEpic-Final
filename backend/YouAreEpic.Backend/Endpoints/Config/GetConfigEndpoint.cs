using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using YouAreEpic.Backend.Configuration;

namespace YouAreEpic.Backend.Endpoints.Config
{
    public class ConfigResponse
    {
        public string PublishableKey { get; set; }
    }

    public class GetConfigEndpoint : EndpointBaseAsync
      .WithoutRequest
      .WithResult<ConfigResponse>
    {
        private readonly IOptions<StripeOptions> stripeOptions;


        public GetConfigEndpoint(IOptions<StripeOptions> stripeOptions)
        {
            this.stripeOptions = stripeOptions;
        }

        [HttpGet("api/stripe/config")]
        public override Task<ConfigResponse> HandleAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new ConfigResponse
            {
                PublishableKey = stripeOptions.Value.PublishableKey
            });
        }
    }
}
