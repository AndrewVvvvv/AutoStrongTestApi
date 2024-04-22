using AutoStrongTestApi.Models;

namespace AutoStrongTestApi.Interfaces
{
    public interface IStorageService
    {
        Task SaveFile<T>(T obj) where T : BaseModel;

        Task<T?> GetFile<T>(Guid id);

        Task<IEnumerable<T>> GetFiles<T>();

        void DeleteFile(Guid id);
    }
}
