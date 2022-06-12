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
    [BsonCollection("categories")]
    public class Category : Document
    {
        public string ImageLink { get; set; }

        public string Name { get; set; }
    }
}
