using BlobStorageRepositoryDemo.Models;
using BlobStorageRepositoryDemo.Repository;

namespace BlobStorageRepositoryDemo.Service;

public class TodoService : AzureBlobStorageRepositoryClient<Todo>, ITodoService
{
    public TodoService(ITodoAzureBlobStorageRepository repository) : base (repository)
    {        
    }
}