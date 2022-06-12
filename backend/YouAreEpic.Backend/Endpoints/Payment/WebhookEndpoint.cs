using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YouAreEpic.Backend.Configuration;

namespace YouAreEpic.Backend.Endpoints.Payment
{
    public class WebhookEndpoint : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult
    {
        private readonly IOptions<StripeOptions> stripeOptions;

        public WebhookEndpoint(IOptions<StripeOptions> stripeOptions)
        {
            this.stripeOptions = stripeOptions;
        }

        [HttpPost("api/payment/webhook")]
        public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
        {
            var request = HttpContext.Request;
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            Event stripeEvent;
            try
            {
                stripeEvent = EventUtility.ConstructEvent(
                    json,
                    request.Headers["Stripe-Signature"],
                    stripeOptions.Value.WebhookSecret
                );
               Console.WriteLine($"Webhook notification with type: {stripeEvent.Type} found for {stripeEvent.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something failed:{ex.Message}");
                return BadRequest();
            }

            if (stripeEvent.Type == "payment_intent.succeeded")
            {
                var paymentIntent = stripeEvent.Data.Object as Stripe.PaymentIntent;

               
               Console.WriteLine($"PaymentIntent ID: {paymentIntent.Id}");
                // Take some action based on session.
            }

            if (stripeEvent.Type == "payment_intent.created")
            {
                var paymentIntent = stripeEvent.Data.Object as Stripe.PaymentIntent;
                Console.WriteLine($"PaymentIntent ID: {paymentIntent.Id}");
                // Take some action based on session.
            }


            return Ok();
        }
    }
}
