using AutoStrongTestApi.Dto;
using AutoStrongTestApi.Models;

namespace AutoStrongTestApi.Interfaces
{
    public interface IPostService
    {
        Task<Post> CreatePostAsync(Post post);

        Task<Post?> GetPost(Guid id);

        Task<IEnumerable<Post>> GetPosts();

        Task<Post?> UpdatePostAsync(Post post);

        void DeletePost(Guid id);
    }
}
