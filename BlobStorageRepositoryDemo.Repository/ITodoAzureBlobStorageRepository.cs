using BlobStorageRepositoryDemo.Models;

namespace BlobStorageRepositoryDemo.Repository;

public interface ITodoAzureBlobStorageRepository : IAzureStorageRepository<Todo>
{
}