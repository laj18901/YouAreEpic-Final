using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YouAreEpic.Backend.Repository.Implementations;

namespace YouAreEpic.Backend.Endpoints.Payment
{
    public class CheckoutSessionEndpoint : EndpointBaseAsync
        .WithRequest<string>
        .WithActionResult
    {

        private readonly INonprofitorganisationRepository ngoRepo;

        public CheckoutSessionEndpoint(INonprofitorganisationRepository ngoRepo)
        {
            this.ngoRepo = ngoRepo;
        }

        [HttpGet("api/payment/checkout-session")]
        public override async Task<ActionResult> HandleAsync([FromQuery] string sessionId, CancellationToken cancellationToken = default)
        {
            var service = new SessionService();

            var options = new SessionListLineItemsOptions();
            var lineItem = (await service.ListLineItemsAsync(sessionId,options)).First();
            var ngoid = (await ngoRepo.FindOneAsync(n => n.Name == lineItem.Description)).Id;

            return Ok(new { ngoid = ngoid.ToString(), ammount = lineItem.AmountTotal });
        }
    }
}
