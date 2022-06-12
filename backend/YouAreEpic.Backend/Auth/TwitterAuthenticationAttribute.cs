using LinqToTwitter;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouAreEpic.Backend.Auth
{
    public class TwitterAuthenticationAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var auth = new MvcAuthorizer
            {
                CredentialStore = new SessionStateCredentialStore(context.HttpContext.Session)
            };

            if (auth.CredentialStore.HasAllCredentials())
            {
                await next();
            }
            else
            {
                context.HttpContext.Response.StatusCode = 401;
            }
        }
    }
}
