using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YouAreEpic.Backend.Dtos;
using YouAreEpic.Backend.Repository.Implementations;

namespace YouAreEpic.Backend.Endpoints.Posts
{

    public class GetAllPostsResponse
    {
        public List<PostRepsonseDto> Posts { get; set; }
    }

    public class GetAllPostsEndpoints : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<GetAllPostsResponse>
    {
        private readonly IPostRepository postRepository;

        public GetAllPostsEndpoints(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        [HttpGet("api/posts", Name = "posts_get_all")]
        public override async Task<ActionResult<GetAllPostsResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            return new GetAllPostsResponse
            {
                Posts = (await postRepository.GetAll())
                     .Select(p => new PostRepsonseDto
                     {
                         ImageLink = p.ImageLink,
                         NonprofitorganisationId = p.NonprofitorganisationId.ToString(),
                         Text = p.Text,
                         VideoLink = p.VideoLink
                     }).ToList()
            };
        }
    }
}
