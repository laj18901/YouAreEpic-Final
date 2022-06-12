using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YouAreEpic.Backend.Repository.Implementations;

namespace YouAreEpic.Backend.Endpoints.Nonprofitorganisations
{
    public class CreateNPORequest
    {
        [Required]
        public string LogoLink { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string WebsiteLink { get; set; }

        [Required]
        public IList<string> CategoryIds { get; set; }
    }

    public class CreateNPOResponse
    {
        public string Id { get; set; }
    }

    public class CreateNPOEndpoint : EndpointBaseAsync
        .WithRequest<CreateNPORequest>
        .WithActionResult<CreateNPOResponse>
    {
        private readonly INonprofitorganisationRepository nonprofitorganisationRepository;

        public CreateNPOEndpoint(INonprofitorganisationRepository nonprofitorganisationRepository)
        {
            this.nonprofitorganisationRepository = nonprofitorganisationRepository;
        }

        [HttpPost("api/npos", Name = "npos_create")]
        public override async Task<ActionResult<CreateNPOResponse>> HandleAsync([FromBody] CreateNPORequest request, CancellationToken cancellationToken = default)
        {
            var result = await nonprofitorganisationRepository.InsertOneAsync(new Models.Nonprofitorganisation
            {
                CategoryIds = request.CategoryIds.Select(cid => new ObjectId(cid)).ToList(),
                Description = request.Description,
                LogoLink = request.LogoLink,
                Name = request.Name,
                ShortDescription = request.ShortDescription,
                WebsiteLink = request.WebsiteLink
            });

            var response = new CreateNPOResponse
            {
                Id = result.Id.ToString()
            };


            return CreatedAtRoute("npos_get", new { id = result.Id }, response);
        }
    }
}
