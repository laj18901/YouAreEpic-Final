using LinqToTwitter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using YouAreEpic.Backend.Auth;
using YouAreEpic.Backend.Dtos;
using YouAreEpic.Backend.Models;
using YouAreEpic.Backend.Repository.Implementations;

namespace YouAreEpic.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TwitterController : EnhancedControllerBase
    {

        private readonly INonprofitorganisationRepository ngoRepo;

        public TwitterController(INonprofitorganisationRepository ngoRepo, IConfiguration config) : base(config)
        {
            this.ngoRepo = ngoRepo;
        }

       
        [TwitterAuthentication]
        [HttpPost("Tweet")]
        public async Task<IActionResult> Tweet([FromForm] List<IFormFile> files, [FromForm] string text, [FromForm] decimal ammount, [FromForm] string ngoid)
        {
            var auth = new MvcAuthorizer
            {
                CredentialStore = new SessionStateCredentialStore(HttpContext.Session)
            };

            var ctx = new TwitterContext(auth);

            var imageUploadTasks = new List<Task<Media>>();
             
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    
                    using (var ms = new MemoryStream())
                    {
                        byte[] bytes;
                        await formFile.CopyToAsync(ms);
                        bytes = ms.ToArray();

                        if (formFile.ContentType.ToLower() == "image/jpg" ||
                            formFile.ContentType.ToLower() == "image/jpeg" ||
                            formFile.ContentType.ToLower() == "image/png")
                        {
                            imageUploadTasks.Add(ctx.UploadMediaAsync(bytes, formFile.ContentType.ToLower(), "tweet_image"));
                        }
                        if(formFile.ContentType.ToLower() == "video/mp4")
                        {
                            imageUploadTasks.Add(ctx.UploadMediaAsync(bytes, "video/mp4", "tweet_video"));
                        }
                    }
                    
                }
            }

           
            await Task.WhenAll(imageUploadTasks);

            await Task.Delay(5000);

            List<string> mediaIds =
                (from tsk in imageUploadTasks
                 select tsk.Result.MediaID.ToString())
                .ToList();

            var ngo = await ngoRepo.FindByIdAsync(new ObjectId(ngoid));
            string status = GeneratedTweetText(text, ngo, ammount);

            Tweet tweet = await ctx.TweetMediaAsync(status, mediaIds);
            await Task.Delay(2000);
           
            return Ok(new
            {
                Name = auth.CredentialStore.ScreenName,
                Id = tweet.ID
            });
        }


        public string GeneratedTweetText(string text, Nonprofitorganisation ngo, decimal ammount)
        {
            return $@"{text ?? ""}

Mit diesem Post wurde @{ngo.TwitterUsername} mit einer Spende von {ammount}€ unterstützt.

YOU ARE EPIC!";
        }
    }
}
