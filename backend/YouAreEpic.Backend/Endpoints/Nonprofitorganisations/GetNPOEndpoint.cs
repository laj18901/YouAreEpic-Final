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

namespace YouAreEpic.Backend.Endpoints.Nonprofitorganisations
{
    public class GetNPOResponse
    {
        public string LogoLink { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public string WebsiteLink { get; set; }

        public List<CategoryResponseDto> Categories { get; set; }
    }

    public class GetNPOEndpoint : EndpointBaseAsync
      .WithRequest<string>
      .WithActionResult<GetNPOResponse>
    {

        private readonly INonprofitorganisationRepository nonprofitorganisationRepository;
        private readonly ICategoryRepository categoryRepository;

        public GetNPOEndpoint(INonprofitorganisationRepository nonprofitorganisationRepository, ICategoryRepository categoryRepository)
        {
            this.nonprofitorganisationRepository = nonprofitorganisationRepository;
            this.categoryRepository = categoryRepository;
        }

        [HttpGet("api/npos/{id}", Name = "npos_get")]
        public override async Task<ActionResult<GetNPOResponse>> HandleAsync(string id, CancellationToken cancellationToken = default)
        {
            var result = await nonprofitorganisationRepository.FindByIdAsync(new ObjectId(id));

            var categories = await categoryRepository.FindManyByIdAsync(result.CategoryIds);

            return new GetNPOResponse
            {
                Description = result.Description,
                LogoLink = result.LogoLink,
                Name = result.Name,
                ShortDescription = result.ShortDescription,
                WebsiteLink = result.WebsiteLink,

                Categories = categories.Select(c => new CategoryResponseDto 
                { 
                    ImageLink = c.ImageLink,
                    Name = c.Name
                }).ToList()
            };
        }
    }
}
