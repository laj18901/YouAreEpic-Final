using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YouAreEpic.Backend.Models;
using YouAreEpic.Backend.Repository.Implementations;

namespace YouAreEpic.Backend.Endpoints.Nonprofitorganisations
{
    public class GetAllNPOsResponse
    {
        public List<NPOResonse> NPOs { get; set; }
    }

    public class NPOResonse
    {
        public string Id { get; set; }
        public string LogoLink { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public string WebsiteLink { get; set; }

        public List<string> CategoryIds { get; set; }
    }

    public class NPORequest
    {
        public List<string> Categories { get; set; }
    }

    public class GetAllNPOsEndpoint : EndpointBaseAsync
        .WithRequest<NPORequest>
        .WithActionResult<GetAllNPOsResponse>
    {
        private readonly INonprofitorganisationRepository nonprofitorganisationRepository;

        public GetAllNPOsEndpoint(INonprofitorganisationRepository nonprofitorganisationRepository)
        {
            this.nonprofitorganisationRepository = nonprofitorganisationRepository;
        }

        [HttpPost("api/npos-by-category", Name = "npos_get_all")]
        public override async Task<ActionResult<GetAllNPOsResponse>> HandleAsync([FromBody] NPORequest data, CancellationToken cancellationToken = default)
        {
            List<Nonprofitorganisation> result = (await nonprofitorganisationRepository.GetAll()).ToList();

            if (data?.Categories != null)
            {
                var categories = data.Categories.Select(c => new ObjectId(c));
                var filtered = result.Where(ngo => ngo.CategoryIds.Any(c => categories.Contains(c))).ToList();
                if (filtered.Any())
                {
                    result = filtered;
                }
            }
           
            

            return new GetAllNPOsResponse
            {
                NPOs = result.Select(npo => new NPOResonse
                {
                    Id = npo.Id.ToString(),
                    CategoryIds = npo.CategoryIds.Select(id=> id.ToString()).ToList(),
                    Description = npo.Description,
                    LogoLink = npo.LogoLink,
                    Name = npo.Name,
                    ShortDescription = npo.ShortDescription,
                    WebsiteLink = npo.WebsiteLink,
                }).ToList()
            };
        }
    }
}
