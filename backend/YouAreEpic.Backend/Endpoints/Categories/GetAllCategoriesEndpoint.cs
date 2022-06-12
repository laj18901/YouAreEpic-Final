using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YouAreEpic.Backend.Dtos;
using YouAreEpic.Backend.Endpoints.Posts;
using YouAreEpic.Backend.Repository.Implementations;

namespace YouAreEpic.Backend.Endpoints.Categories
{
    public class GetAllCategoriesResponse
    {
        public List<CategoryResponseDto> Categories { get; set; }
    }

    public class GetAllCategoriesEndpoint : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<GetAllCategoriesResponse>
    {
        private readonly ICategoryRepository categoryRepository;

        public GetAllCategoriesEndpoint(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet("api/categories", Name = "categories_get_all")]
        public override async Task<ActionResult<GetAllCategoriesResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            return new GetAllCategoriesResponse
            {
                Categories = (await categoryRepository.GetAll())
                     .Select(c => new CategoryResponseDto
                     {
                        Id = c.Id.ToString(),
                        ImageLink = c.ImageLink,
                        Name = c.Name
                     }).ToList()
            };
        }
    }
}
