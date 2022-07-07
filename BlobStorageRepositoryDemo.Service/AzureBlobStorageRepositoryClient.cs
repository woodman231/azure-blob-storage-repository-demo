using BlobStorageRepositoryDemo.Models;
using BlobStorageRepositoryDemo.Repository;

namespace BlobStorageRepositoryDemo.Service;

public abstract class AzureBlobStorageRepositoryClient<T> : IAzureStorageRepository<T> where T : IBaseAzureStorageEntityModel
{
    private readonly IAzureStorageRepository<T> _blobStorageRepository;

    public AzureBlobStorageRepositoryClient(IAzureStorageRepository<T> blobStorageRepository)
    {
        _blobStorageRepository = blobStorageRepository;
    }

    public Task DeletAsync(string id)
    {
        try {
            return _blobStorageRepository.DeletAsync(id);
        }
        catch {
            // Do nothing
        }

        return Task.CompletedTask;
    }

    public async Task<List<T>?> GetAllAsync()
    {
        try {
            return await _blobStorageRepository.GetAllAsync();
        }
        catch {
            // Do nothing, return null
        }

        return null;
    }

    public async Task<T?> GetOneAsync(string id)
    {
        try {
            return await _blobStorageRepository.GetOneAsync(id);
        }
        catch {
            // Do nothing, return null
        }

        return default(T);
    }

    public async Task<T?> UpsertAsync(T entityDetails)
    {
        try {
            var results = await _blobStorageRepository.UpsertAsync(entityDetails);

            return results;
        }
        catch {
            // Do nothing, return null
        }

        return default(T);
    }
}