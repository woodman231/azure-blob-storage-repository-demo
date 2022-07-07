using BlobStorageRepositoryDemo.Models;

namespace BlobStorageRepositoryDemo.Service;

public interface ITodoService : IAzureStorageRepositoryClient<Todo>
{
}