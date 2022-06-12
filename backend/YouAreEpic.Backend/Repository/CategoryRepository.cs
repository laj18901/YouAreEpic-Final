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

    public interface ICategoryRepository : IMongoDBRepository<ObjectId,Category>, IRepository<ObjectId, Category>
    {
        public Task<List<Category>> Seed();

        public Task<List<Category>> FindManyByIdAsync(List<ObjectId> ids);

    }

    public class CategoryRepository : MongoDBRepository<ObjectId, Category>, ICategoryRepository
    {
        public CategoryRepository(MongoSettings settings) : base(settings) { }

        public async Task<List<Category>> FindManyByIdAsync(List<ObjectId> ids)
        {
            return (await this.FilterByAsync(c => ids.Contains(c.Id))).ToList();
        }

        public async Task<List<Category>> Seed()
        {
            //For testing
            /**var _fakeCategories = new Faker<Category>("de")
            .CustomInstantiator(f =>
            {
                var name = f.Lorem.Word();
                var imageLink = f.Image.PlaceImgUrl();

                return new Category
                {
                    ImageLink = imageLink,
                    Name = name
                };
            })
            .Generate(10)
            .ToList();
            **/

            var _categories = new List<Category>
            {
                new Category 
                { 
                    Name = "Kinder",
                    ImageLink = "https://cdn-icons-png.flaticon.com/512/4540/4540539.png"
                },
                new Category
                {
                    Name = "Tiere",
                    ImageLink = "https://cdn-icons-png.flaticon.com/512/2362/2362224.png"
                },
                new Category
                {
                    Name = "Umwelt",
                    ImageLink = "https://cdn-icons-png.flaticon.com/512/1598/1598238.png"
                },
                new Category
                {
                    Name = "Forschung",
                    ImageLink = "https://cdn-icons-png.flaticon.com/512/4599/4599811.png"
                },
             };


            return await InsertManyAsync(_categories);
        }
    }
}
