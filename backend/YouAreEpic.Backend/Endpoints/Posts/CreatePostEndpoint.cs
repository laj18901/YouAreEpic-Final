using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YouAreEpic.Backend.Repository.Implementations;
using YouAreEpic.Backend.Services;

namespace YouAreEpic.Backend.Endpoints.Posts
{
    public class CreatePostRequest
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public string ImageLink { get; set; }

        [Required]
        public string VideoLink { get; set; }

        public string NonprofitorganisationId { get; set; }
    }

    public class CreatePostResponse
    {
        public string Id { get; set; } = null!;
    }

    public class CreatePostEndpoint : Ardalis.ApiEndpoints.EndpointBaseAsync
        .WithRequest<CreatePostRequest>
        .WithActionResult<CreatePostResponse>
    {
        private readonly PostService postService;

        public CreatePostEndpoint(PostService postService)
        {
            this.postService = postService;
        }

        [HttpPost("api/posts")]
        public override async Task<ActionResult<CreatePostResponse>> HandleAsync([FromBody] CreatePostRequest request, CancellationToken cancellationToken = default)
        {
            var result = await postService.CreatePostAsync(new Dtos.PostDto 
            {
                ImageLink = request.ImageLink,
                NonprofitorganisationId = new ObjectId(request.NonprofitorganisationId),
                Text = request.Text,
                VideoLink = request.VideoLink
            });

            var response = new CreatePostResponse
            {
                Id = result.Id.ToString()
            };

            return CreatedAtRoute("posts_get", new { id = result.Id }, response);
        }
    }
}
