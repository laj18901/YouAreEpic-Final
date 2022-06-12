using LinqToTwitter;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YouAreEpic.Backend.Services;

namespace YouAreEpic.Backend.Controllers
{
    [Route("api/[controller]")]
    public class OAuth2Controller : EnhancedControllerBase
    {
        private readonly TwitterConfigurationService _twitterConfig;

        public OAuth2Controller(TwitterConfigurationService twitterConfig, IConfiguration config) : base(config)
        {
            _twitterConfig = twitterConfig;
        }


        [HttpGet("Login")]
        public async Task<ActionResult> BeginAsync()
        {
            string twitterCallbackUrl = _twitterConfig.RedirectEndpoint;

            var auth = new MvcOAuth2Authorizer
            {
                CredentialStore = new OAuth2SessionCredentialStore(HttpContext.Session)
                {
                    ClientID = _twitterConfig.ClientId,
                    ClientSecret = _twitterConfig.ClientSecret,
                    Scopes = new List<string>
                    {
                        "tweet.read",
                        "tweet.write",
                        "tweet.moderate.write",
                        "users.read",
                        "follows.read",
                        "follows.write",
                        "offline.access",
                        "space.read",
                        "mute.read",
                        "mute.write",
                        "like.read",
                        "like.write",
                        "block.read",
                        "block.write"
                    },
                    RedirectUri = twitterCallbackUrl,
                }
            };

            return await auth.BeginAuthorizeAsync(Guid.NewGuid().ToString("N"));
        }

        [HttpGet("LoginRedirect")]
        public async Task<ActionResult> CompleteAsync()
        {
            var auth = new MvcOAuth2Authorizer
            {
                CredentialStore = new OAuth2SessionCredentialStore(HttpContext.Session)
            };

            Request.Query.TryGetValue("code", out StringValues code);
            Request.Query.TryGetValue("state", out StringValues state);

            await auth.CompleteAuthorizeAsync(code, state);

            return RedirectToFrontend("/");
        }

        [HttpGet("Logout")]
        public async Task<ActionResult> Logout()
        {
            var auth = new MvcOAuth2Authorizer
            {
                CredentialStore = new OAuth2SessionCredentialStore(HttpContext.Session)
            };

            await auth.CredentialStore.ClearAsync();
            HttpContext.Session.Clear();
            return Ok();
        }
    }
}
