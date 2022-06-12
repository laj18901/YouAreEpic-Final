using LinqToTwitter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouAreEpic.Backend.Controllers
{
    public class EnhancedControllerBase : ControllerBase
    {
        private readonly IConfiguration _config;

        public EnhancedControllerBase(IConfiguration config)
        {
            _config = config;
        }

        public RedirectResult RedirectToFrontend(string path)
        {
            var url = _config.GetSection("Server:FrontEndDomain").Value;

            return Redirect($"{url}{path}");
        }

        public async Task<TwitterUser> GetAuthenticatedUser()
        {
            if(!HttpContext.Session.Keys.Any()) return null;

            var auth = new MvcAuthorizer
            {
                CredentialStore = new SessionStateCredentialStore(HttpContext.Session)
            };

            var ctx = new TwitterContext(auth);

            var userQuery =
               await
               (from usr in ctx.TwitterUser
                where usr.Type == UserType.UsernameLookup &&
                      usr.Usernames == auth.CredentialStore.ScreenName
                        select usr)
               .SingleOrDefaultAsync();

            TwitterUser user = userQuery.Users.FirstOrDefault();


            return user;
        }
    }
}
