using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YouAreEpic.Backend.Dtos;
using YouAreEpic.Backend.Models;
using YouAreEpic.Backend.Repository.Implementations;

namespace YouAreEpic.Backend.Endpoints.Posts
{
    public class GetPostResponse
    {
        public string Text { get; set; }

        public string ImageLink { get; set; }

        public string VideoLink { get; set; }

        public NonprofitorganisationResponseDto Nonprofitorganisation { get; set; }
    }

    public class GetPostEndpoint : EndpointBaseAsync
      .WithRequest<string>
      .WithActionResult<GetPostResponse>
    {
        private readonly IPostRepository postRepository;
        private readonly INonprofitorganisationRepository nonprofitorganisationRepository;
        private readonly ICategoryRepository categoryRepository;

        public GetPostEndpoint(IPostRepository postRepository, INonprofitorganisationRepository nonprofitorganisationRepository, ICategoryRepository categoryRepository)
        {
            this.postRepository = postRepository;
            this.nonprofitorganisationRepository = nonprofitorganisationRepository;
            this.categoryRepository = categoryRepository;
        }

        [HttpGet("api/posts/{id}", Name = "posts_get")]
        public override async Task<ActionResult<GetPostResponse>> HandleAsync(string id, CancellationToken cancellationToken = default)
        {
            var post = await postRepository.FindByIdAsync(new ObjectId(id));

            var npo = await nonprofitorganisationRepository.FindByIdAsync(post.NonprofitorganisationId);

            var categories = categoryRepository.AsQueryable()
                                                .Where(c => npo.CategoryIds.Contains(c.Id))
                                                 .ToList();

            return new GetPostResponse
            {
                Nonprofitorganisation = new NonprofitorganisationResponseDto 
                {
                    Categories = categories.Select(c => new CategoryResponseDto 
                    { 
                        ImageLink = c.ImageLink,
                        Name = c.Name
                    }).ToList(),

                    Description = npo.Description,
                    LogoLink = npo.LogoLink,
                    Name = npo.Name,
                    ShortDescription = npo.ShortDescription,
                    WebsiteLink = npo.WebsiteLink
                },
                ImageLink = post.ImageLink,
                Text = post.Text,
                VideoLink = post.VideoLink
            }; 
        }
    }
}
