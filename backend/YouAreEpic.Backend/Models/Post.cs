using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using YouAreEpic.Backend.MongoDB;

namespace YouAreEpic.Backend.Models
{
    [BsonCollection("posts")]
    public class Post : Document
    {
        public string Text { get; set; }

        public string ImageLink { get; set; }

        public string VideoLink { get; set; }

        public ObjectId NonprofitorganisationId { get; set; }

        [BsonIgnore]
        public string GeneratedText => $"{Text}-abc";
    }
}
