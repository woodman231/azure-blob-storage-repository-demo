using System.Text.Json;
using Azure.Storage.Blobs;
using BlobStorageRepositoryDemo.Models;

namespace BlobStorageRepositoryDemo.Repository;

public abstract class AzureBlobStorageRepository<T> : IAzureStorageRepository<T> where T : IBaseAzureStorageEntityModel
{
    private readonly IBlobStorageService _blobStorageService;
    private readonly string _folderName;

    public AzureBlobStorageRepository(BlobServiceClient blobServiceClient, string containerName, string folderName)
    {
        _blobStorageService = new BlobStorageService(blobServiceClient, containerName);
        _folderName = folderName;
    }

    public async Task DeletAsync(string id)
    {
        string filePath = GetFilePath(id);

        await _blobStorageService.DeleteDocumentAsync(filePath);
    }

    public async Task<List<T>?> GetAllAsync()
    {
        try {
            List<string>? filesInFolder = await _blobStorageService.GetListOfBlobsInFolderAsync(_folderName);

            if(filesInFolder is not null) {
                var results = new List<T>();

                foreach(var file in filesInFolder) {
                    var id = GetIdFromFilePath(file);
                    try {
                        var result = await this.GetOneAsync(id);
                        if(result is not null) {
                            results.Add(result);
                        }
                    }
                    catch {
                        // Do nothing, try to get the next one
                    }
                }

                return results;
            }
        }
        catch {
            // Do nothing, will return null
        }

        return null;
    }

    public async Task<T?> GetOneAsync(string id)
    {
        try {
            string filePath = GetFilePath(id);

            var blobContent = await _blobStorageService.GetBlobContentAsync(filePath);

            var contentAsObject = JsonSerializer.Deserialize<T>(blobContent);

            return contentAsObject;
        }
        catch {
            // Do nothing, will return null
        }

        return default(T);
    }

    public async Task<T?> UpsertAsync(T entityDetails)
    {
        try {
            string filePath = GetFilePath(entityDetails.Id);

            var entityDetailsAsString = JsonSerializer.Serialize(entityDetails, new JsonSerializerOptions { WriteIndented = true });

            if(entityDetails is not null) {
                var entityDetailsAsStream = new MemoryStream();
                var streamWriter = new StreamWriter(entityDetailsAsStream);
                streamWriter.Write(entityDetailsAsString);
                streamWriter.Flush();
                entityDetailsAsStream.Position = 0;

                await _blobStorageService.UpdateBlobContentAsync(filePath, entityDetailsAsStream);

                return entityDetails;
            }
        }
        catch {
            // Do nothing, return null
        }

        return default(T);
    }

    private string GetFilePath(string id)
    {
        string filePath = string.Join('/', _folderName, $"{id}.json");

        return filePath;
    }

    private string GetIdFromFilePath(string filePath)
    {
        string[] fileParts = filePath.Split('/');
        string lastPart = fileParts.Last();
        string[] fileNameParts = lastPart.Split('.');
        string idPart = fileNameParts.First();

        return idPart;
    }
}