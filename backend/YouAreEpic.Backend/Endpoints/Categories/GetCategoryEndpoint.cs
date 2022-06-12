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

namespace YouAreEpic.Backend.Endpoints.Categories
{
    public class GetCategoryEndpoint : EndpointBaseAsync
      .WithRequest<string>
      .WithActionResult<CategoryResponseDto>
    {

        private readonly ICategoryRepository categoryRepository;

        public GetCategoryEndpoint(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet("api/categories/{id}", Name = "categories_get")]
        public override async Task<ActionResult<CategoryResponseDto>> HandleAsync(string id, CancellationToken cancellationToken = default)
        {
            var result = await categoryRepository.FindByIdAsync(new ObjectId(id));

            return new CategoryResponseDto
            {
                ImageLink = result.ImageLink,
                Name = result.Name
            };
        }
    }
}
