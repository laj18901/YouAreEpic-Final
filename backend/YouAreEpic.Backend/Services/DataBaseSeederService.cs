using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using YouAreEpic.Backend.Repository.Implementations;

namespace YouAreEpic.Backend.Services
{
    public class DataBaseSeederService : BackgroundService
    {
        private readonly IPostRepository postRepository;
        private readonly INonprofitorganisationRepository nonprofitorganisationRepository;
        private readonly ICategoryRepository categoryRepository;

        public DataBaseSeederService(IPostRepository postRepository, INonprofitorganisationRepository nonprofitorganisationRepository, ICategoryRepository categoryRepository)
        {
            this.postRepository = postRepository;
            this.nonprofitorganisationRepository = nonprofitorganisationRepository;
            this.categoryRepository = categoryRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await postRepository.DropCollectionAsync();
            await nonprofitorganisationRepository.DropCollectionAsync();
            await categoryRepository.DropCollectionAsync();

            var categories = await categoryRepository.Seed();
            var npos = await nonprofitorganisationRepository.Seed(categories);
            var posts = await postRepository.Seed(npos);
        }
    }
}
