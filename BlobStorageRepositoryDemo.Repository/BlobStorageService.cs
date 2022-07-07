using Azure.Storage.Blobs;

namespace BlobStorageRepositoryDemo.Repository;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _containerName;

    public BlobStorageService(BlobServiceClient blobServiceClient, string containerName)
    {
        _blobServiceClient = blobServiceClient;
        _containerName = containerName;
    }

    public async Task DeleteDocumentAsync(string blobName)
    {
        var containerClient = await GetContainerClientAsync();

        var blobClient = containerClient.GetBlobClient(blobName);

        await blobClient.DeleteAsync();
    }

    public async Task<Stream> GetBlobContentAsync(string blobName)
    {
        var containerClient = await GetContainerClientAsync();

        var blobClient = containerClient.GetBlobClient(blobName);

        return await blobClient.OpenReadAsync();
    }

    public async Task<List<string>?> GetListOfBlobsInFolderAsync(string folderName)
    {
        var results = new List<string>();

        var containerClient = await GetContainerClientAsync();

        var blobsInFolder = containerClient.GetBlobs(prefix: folderName);

        if(blobsInFolder is not null) {
            foreach(var blob in blobsInFolder) {
                results.Add(blob.Name);
            }

            return results;
        }

        return null;
    }

    public async Task UpdateBlobContentAsync(string blobName, Stream content)
    {
        var containerClient = await GetContainerClientAsync();

        var blobClient = containerClient.GetBlobClient(blobName);

        await blobClient.UploadAsync(content, true);
    }

    private async Task<BlobContainerClient> GetContainerClientAsync()
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

        await blobContainerClient.CreateIfNotExistsAsync();

        return blobContainerClient;
    }
}