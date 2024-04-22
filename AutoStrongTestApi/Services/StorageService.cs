using AutoStrongTestApi.Interfaces;
using AutoStrongTestApi.Models;
using System.Text.Json;

namespace AutoStrongTestApi.Services
{
    public class StorageService(IConfiguration configuration) : IStorageService
    {
        private const string PostPathKey = "PostPath";
        private const string JsonSearchPattern = "*.json";
        private const string JsonExtensions = ".json";

        private readonly string PostsPath = configuration.GetValue<string>(PostPathKey)!;

        public async Task SaveFile<T>(T obj) where T : BaseModel
        {
            CreateDirectoryIfNotExists();

            var filename = string.Concat(obj.Id, JsonExtensions);
            var path = Path.Join(PostsPath, filename);
            await using var createStream = File.Create(path);
            await JsonSerializer.SerializeAsync(createStream, obj);
        }

        public async Task<T?> GetFile<T>(Guid id)
        {
            CreateDirectoryIfNotExists();

            var path = Path.Join(PostsPath, id.ToString());
            await using var stream = File.OpenRead(path);
            return await JsonSerializer.DeserializeAsync<T>(stream);
        }

        public async Task<IEnumerable<T>> GetFiles<T>()
        {
            List<T> files = [];
            foreach (var file in Directory.GetFiles(PostsPath, JsonSearchPattern))
            {
                using var stream = File.OpenRead(file);
                T? obj = await JsonSerializer.DeserializeAsync<T>(stream);
                
                if (obj is not null)
                {
                    files.Add(obj);
                }
            }

            return files;
        }

        public void DeleteFile(Guid id)
        {
            CreateDirectoryIfNotExists();
            var path = Path.Join(PostsPath, id.ToString());

            File.Delete(path);
        }

        private void CreateDirectoryIfNotExists()
        {
            if (Directory.Exists(PostsPath))
            {
                Directory.CreateDirectory(PostsPath);
            }
        }
    }
}
