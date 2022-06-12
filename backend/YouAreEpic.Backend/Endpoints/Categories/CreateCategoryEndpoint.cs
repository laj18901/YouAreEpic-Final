using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YouAreEpic.Backend.Repository.Implementations;

namespace YouAreEpic.Backend.Endpoints.Categories
{
    public class CreateCategoryRequest
    {
        [Required]
        public string ImageLink { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class CreateCategoryResponse
    {
        public string Id { get; set; } = null!;
    }
    public class CreateCategoryEndpoint : EndpointBaseAsync
        .WithRequest<CreateCategoryRequest>
        .WithActionResult<CreateCategoryResponse>
    {
        private readonly ICategoryRepository categoryRepository;

        public CreateCategoryEndpoint(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpPost("api/categories")]
        public override async Task<ActionResult<CreateCategoryResponse>> HandleAsync([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken = default)
        {
            var result = await categoryRepository.InsertOneAsync(new Models.Category 
            { 
                ImageLink = request.ImageLink,
                Name = request.Name
            });

            var response = new CreateCategoryResponse
            {
                Id = result.Id.ToString()
            };

            return CreatedAtRoute("categories_get", new { id = result.Id }, response);
        }
    }
}
