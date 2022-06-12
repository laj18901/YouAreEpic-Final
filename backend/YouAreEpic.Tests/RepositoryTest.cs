using MongoDB.Bson;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using YouAreEpic.Backend.Models;
using YouAreEpic.Backend.MongoDB;
using YouAreEpic.Backend.Repository.Implementations;

namespace YouAreEpic.Tests
{
    public class RepositoryTest
    {
        [Fact]
        public async Task AsQueryableTest()
        {

            var postRepo = new PostRepository(
                new MongoSettings("mongodb://dbi-db:LLT6m6DfawL53g60Vvd6MB7CmwCUWNqX6ifiagwqTa6O1eaT9mv9eZuUPFlMqXvW7betJfaLLgGzqRMFCatAaw==@dbi-db.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@dbi-db@", "dbi-db")
                );

            await postRepo.InsertOneAsync(new Post
            {
                Text = "SampleText"
            });

            var text =  (
                        from p in postRepo.AsQueryable()
                        select p.Text
                        )
                        .FirstOrDefault();

            Assert.Equal("SampleText", text);

           await postRepo.DropCollectionAsync();
        }

        [Fact]
        public async Task InsertAsyncTest()
        {
            var categoryRepo = new MongoDBRepository<ObjectId,Category>(
                new MongoSettings("mongodb://dbi-db:LLT6m6DfawL53g60Vvd6MB7CmwCUWNqX6ifiagwqTa6O1eaT9mv9eZuUPFlMqXvW7betJfaLLgGzqRMFCatAaw==@dbi-db.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@dbi-db@", "dbi-db")
                );

            var category = await categoryRepo.InsertOneAsync(new Category
            {
                ImageLink = "TestLink1",
                Name = "Category"
            });


            Assert.NotNull(category.Id.ToString());
            Assert.NotEmpty(category.Id.ToString());
            Assert.Same(category.Name, "Category");
            Assert.Same(category.ImageLink, "TestLink1");

            await categoryRepo.DropCollectionAsync();
        }

        [Fact]
        public async Task FindByIdAsyncTest()
        {
            var categoryRepo = new MongoDBRepository<ObjectId, Category>(
                 new MongoSettings("mongodb://dbi-db:LLT6m6DfawL53g60Vvd6MB7CmwCUWNqX6ifiagwqTa6O1eaT9mv9eZuUPFlMqXvW7betJfaLLgGzqRMFCatAaw==@dbi-db.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@dbi-db@", "dbi-db")
                 );

            var category = await categoryRepo.InsertOneAsync(new Category
            {
                ImageLink = "TestLink1",
                Name = "Category"
            });

            Assert.NotNull(category.Id.ToString());
            Assert.NotEmpty(category.Id.ToString());

            var foundPerson = await categoryRepo.FindByIdAsync(category.Id);

            Assert.Equal("TestLink1", foundPerson.ImageLink);
            Assert.Equal("Category", foundPerson.Name);

            await categoryRepo.DropCollectionAsync();
        }

        [Fact]
        public async Task GetAllAsyncTest()
        {
            var categoryRepo = new CategoryRepository(
                 new MongoSettings("mongodb://dbi-db:LLT6m6DfawL53g60Vvd6MB7CmwCUWNqX6ifiagwqTa6O1eaT9mv9eZuUPFlMqXvW7betJfaLLgGzqRMFCatAaw==@dbi-db.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@dbi-db@", "dbi-db")
                 );

            await categoryRepo.Seed();

            var categories = await categoryRepo.GetAll();

            Assert.NotEmpty(categories);
            Assert.True(categories.Count() == 10);
            Assert.All(categories, (p) =>
            {
                Assert.NotNull(p.Id.ToString());
                Assert.NotEmpty(p.Id.ToString());
            });


            await categoryRepo.DropCollectionAsync();
        }
    }
}
