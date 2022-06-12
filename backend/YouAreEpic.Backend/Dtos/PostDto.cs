using MongoDB.Bson;

namespace YouAreEpic.Backend.Dtos
{
    public class PostDto
    {
        public string Text { get; set; }

        public string ImageLink { get; set; }

        public string VideoLink { get; set; }

        public ObjectId NonprofitorganisationId { get; set; }
    }
}
