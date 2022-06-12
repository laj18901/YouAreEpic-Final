using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouAreEpic.Backend.MongoDB;
using YouAreEpic.Backend.Repository;

namespace YouAreEpic.Backend.Models
{
    [BsonCollection("nonprofitorganisations")]
    public class Nonprofitorganisation : Document
    {
        public string LogoLink { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public string WebsiteLink { get; set; }

        public List<ObjectId> CategoryIds { get; set; }

        public string TwitterUsername { get; set; }

    }
}
