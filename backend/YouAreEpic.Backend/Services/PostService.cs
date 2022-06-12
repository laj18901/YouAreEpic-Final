using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouAreEpic.Backend.Dtos;
using YouAreEpic.Backend.Models;
using YouAreEpic.Backend.Repository.Implementations;

namespace YouAreEpic.Backend.Services
{
    public class PostService
    {
        private readonly IPostRepository postRepository;

        public PostService(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public async Task<Post> CreatePostAsync(PostDto postDto)
        {
            var post = new Post
            {
                VideoLink = postDto.VideoLink,
                Text = postDto.Text,
                ImageLink = postDto.ImageLink,
                NonprofitorganisationId = postDto.NonprofitorganisationId
            };
            return await postRepository.InsertOneAsync(post);
        }
    }
}
