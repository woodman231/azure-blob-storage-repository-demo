using BlobStorageRepositoryDemo.Models;

namespace BlobStorageRepositoryDemo.Repository;

public interface IAzureStorageRepository<T> where T : IBaseAzureStorageEntityModel
{
    Task<T?> UpsertAsync(T entityDetails);
    Task<T?> GetOneAsync(string id);
    Task<List<T>?> GetAllAsync();
    Task DeletAsync(string id);
}