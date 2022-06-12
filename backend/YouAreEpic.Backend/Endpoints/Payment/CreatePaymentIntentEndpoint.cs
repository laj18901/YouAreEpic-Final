using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace YouAreEpic.Backend.Endpoints.Payment
{
    public class CreatePaymentIntentRequest
    {
        [JsonPropertyName("paymentMethodType")]
        public string PaymentMethodType { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }

    public class CreatePaymentResponse
    {
        public string ClientSecret { get; set; }
    }

    public class CreatePaymentIntentEndpoint : EndpointBaseAsync
        .WithRequest<CreatePaymentIntentRequest>
        .WithActionResult<CreatePaymentResponse>
    {
        [HttpPost("api/payment/create-payment-intent")]
        public override async Task<ActionResult<CreatePaymentResponse>> HandleAsync([FromBody] CreatePaymentIntentRequest req, CancellationToken cancellationToken = default)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = 1999,
                Currency = req.Currency,
                PaymentMethodTypes = new List<string> { req.PaymentMethodType }
            };

            // If this is for an ACSS payment, we add payment_method_options to create
            // the Mandate.
            if (req.PaymentMethodType == "acss_debit")
            {
                options.PaymentMethodOptions = new PaymentIntentPaymentMethodOptionsOptions
                {
                    AcssDebit = new PaymentIntentPaymentMethodOptionsAcssDebitOptions
                    {
                        MandateOptions = new PaymentIntentPaymentMethodOptionsAcssDebitMandateOptionsOptions
                        {
                            PaymentSchedule = "sporadic",
                            TransactionType = "personal",
                        },
                    }
                };

            }

            var service = new PaymentIntentService();

            try
            {
                var paymentIntent = await service.CreateAsync(options, cancellationToken: cancellationToken);

                return Ok( new CreatePaymentResponse
                {
                    ClientSecret = paymentIntent.ClientSecret
                });
            }
            catch (StripeException e)
            {
                return BadRequest(new { error = new { message = e.StripeError.Message } });
            }
        }
    }
}
