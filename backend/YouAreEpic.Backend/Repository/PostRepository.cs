using Bogus;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouAreEpic.Backend.Models;
using YouAreEpic.Backend.MongoDB;

namespace YouAreEpic.Backend.Repository.Implementations
{
    public interface IPostRepository : IMongoDBRepository<ObjectId, Post>, IRepository<ObjectId,Post> 
    {
        public Task<List<Post>> Seed(List<Nonprofitorganisation> npos);
    }

    public class PostRepository : MongoDBRepository<ObjectId,Post>, IPostRepository
    {
        public PostRepository(MongoSettings settings) : base(settings) { }

        public async Task<List<Post>> Seed(List<Nonprofitorganisation> npos)
        {
            var _posts = new Faker<Post>("de")
             .CustomInstantiator(f =>
             {
                 var name = f.Lorem.Word();
                 var imageLink = f.Image.PlaceImgUrl();
                 var videoLink = f.Image.PlaceImgUrl();

                 return new Post
                 {
                     ImageLink = imageLink,
                     NonprofitorganisationId = f.PickRandom(npos).Id,
                     Text = f.Lorem.Lines(3),
                     VideoLink = videoLink
                 };
             })
             .Generate(3)
             .ToList();

            return await InsertManyAsync(_posts);
        }
    }
}
