using Azure.Storage.Blobs;
using BlobStorageRepositoryDemo.Models;

namespace BlobStorageRepositoryDemo.Repository;

public class TodoAzureBlobStorageRepository : AzureBlobStorageRepository<Todo>, ITodoAzureBlobStorageRepository
{
    public TodoAzureBlobStorageRepository(BlobServiceClient blobServiceClient) : base(blobServiceClient, "todos", "TodoItems")
    {        
    }
}