using AutoStrongTestApi.Interfaces;
using AutoStrongTestApi.Models;

namespace AutoStrongTestApi.Services
{
    public class PostService(IStorageService storageService) : IPostService
    {
        public async Task<Post> CreatePostAsync(Post post)
        {
            await storageService.SaveFile(post);
            return post;
        }

        public void DeletePost(Guid id)
        {
            storageService.DeleteFile(id);
        }

        public async Task<Post?> GetPost(Guid id)
        {
            return await storageService.GetFile<Post>(id);
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await storageService.GetFiles<Post>();
        }

        public async Task<Post?> UpdatePostAsync(Post post)
        {
            var dbPost = await storageService.GetFile<Post>(post.Id);
            if (dbPost == null)
            {
                return null;
            }

            dbPost.Text = post.Text;
            dbPost.Image = post.Image;

            return dbPost;
        }
    }
}
