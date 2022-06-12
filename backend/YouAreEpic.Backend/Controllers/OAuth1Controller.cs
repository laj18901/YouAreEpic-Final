using LinqToTwitter;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using YouAreEpic.Backend.Auth;
using YouAreEpic.Backend.Services;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace YouAreEpic.Backend.Controllers
{
    [Route("api/[controller]")]
    public class OAuth1Controller : EnhancedControllerBase
    {
        private readonly TwitterConfigurationService _twitterConfig;

        public OAuth1Controller(TwitterConfigurationService twitterConfig, IConfiguration config) : base(config)
        {
            _twitterConfig = twitterConfig;
        }

        [TwitterAuthentication]
        [HttpGet("Me")]
        public async Task<ActionResult> Me()
        {
            return Ok(await this.GetAuthenticatedUser());
        }

        [HttpGet("Login")]
        public async Task<ActionResult> BeginAsync()
        {
            var auth = new MvcAuthorizer
            {
                CredentialStore = new SessionStateCredentialStore(HttpContext.Session)
                {
                    ConsumerKey = _twitterConfig.ConsumerApiKey,
                    ConsumerSecret = _twitterConfig.ConsumerApiSecret
                }
            };

            // Available in v5.1.0 - you can pass parameters that you can read in Complete(), via Request.QueryString, when Twitter returns
            //var parameters = new Dictionary<string, string> { { "my_custom_param", "val" } };
            //string twitterCallbackUrl = Request.GetDisplayUrl().Replace("Begin", "Complete");
            //return await auth.BeginAuthorizationAsync(new Uri(twitterCallbackUrl), parameters);
            //await auth.CredentialStore.ClearAsync();
            string twitterCallbackUrl = _twitterConfig.RedirectEndpoint;
            return await auth.BeginAuthorizationAsync(new Uri(twitterCallbackUrl));
        }

        [HttpGet("LoginRedirect")]
        public async Task<ActionResult> CompleteAsync()
        {
            var auth = new MvcAuthorizer
            {
                CredentialStore = new SessionStateCredentialStore(HttpContext.Session)
            };

            var url = Request.GetDisplayUrl();

            await auth.CompleteAuthorizeAsync(new Uri(url));
           

            // This is how you access credentials after authorization.
            // The oauthToken and oauthTokenSecret do not expire.
            // You can use the userID to associate the credentials with the user.
            // You can save credentials any way you want - database, 
            //   isolated storage, etc. - it's up to you.
            // You can retrieve and load all 4 credentials on subsequent 
            //   queries to avoid the need to re-authorize.
            // When you've loaded all 4 credentials, LINQ to Twitter will let 
            //   you make queries without re-authorizing.
            //
            //var credentials = auth.CredentialStore;
            //string oauthToken = credentials.OAuthToken;
            //string oauthTokenSecret = credentials.OAuthTokenSecret;
            //string screenName = credentials.ScreenName;
            //ulong userID = credentials.UserID;
            //

            return RedirectToFrontend("/");
        }

        [HttpGet("Logout")]
        public async Task<ActionResult> Logout()
        {
            var auth = new MvcAuthorizer
            {
                CredentialStore = new SessionStateCredentialStore(HttpContext.Session)
            };
            await auth.CredentialStore.ClearAsync();
            HttpContext.Session.Clear();
            return Ok();
        }
    }
}
