using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using YouAreEpic.Backend.Configuration;
using YouAreEpic.Backend.Repository.Implementations;

namespace YouAreEpic.Backend.Endpoints.Payment
{
    public class PaymentRequest
    {
        [JsonPropertyName("ngoid")]
        public string ngoid { get; set; }

        [JsonPropertyName("money")]
        public long money { get; set; }
    }

    
    public class CreateCheckoutSessiomEndpoint : EndpointBaseAsync
        .WithRequest<PaymentRequest>
        .WithActionResult
    {
        private readonly IOptions<StripeOptions> stripeOptions;
        private readonly INonprofitorganisationRepository nonprofitorganisationRepository;

        public CreateCheckoutSessiomEndpoint(IOptions<StripeOptions> stripeOptions, INonprofitorganisationRepository nonprofitorganisationRepository)
        {
            this.stripeOptions = stripeOptions;
            this.nonprofitorganisationRepository = nonprofitorganisationRepository;
        }

        [HttpPost("api/payment/create-checkout-session")]
        public override async Task<ActionResult> HandleAsync([FromBody] PaymentRequest request, CancellationToken cancellationToken = default)
        {
            request.money = request.money * 100;

            var ngoid = new ObjectId(request.ngoid);
            var ngo = await nonprofitorganisationRepository.FindByIdAsync(ngoid);

            var context = HttpContext;
            var sessionOptions = new SessionCreateOptions
            {
                SuccessUrl = $"{stripeOptions.Value.Domain}/payment/success?session_id={{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{stripeOptions.Value.Domain}/payment/error",
                Mode = "payment",

                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Quantity = 1,
                        PriceData = new()
                        {
                            ProductData = new()
                            {
                                Name = ngo.Name,
                                Description = ngo.Description,
                                Images = new List<string>(){ ngo.LogoLink },
                                Metadata = new ()
                                {
                                    ["ngoid"] = ngoid.ToString() 
                                }
                            },
                            Currency = "eur",
                            UnitAmount = request.money
                        },
                       
                    },
                },
                // AutomaticTax = new SessionAutomaticTaxOptions { Enabled = true },
            };

            var service = new SessionService();
            var session = await service.CreateAsync(sessionOptions);
            return Ok( new { url = session.Url });
        }
    }
}
