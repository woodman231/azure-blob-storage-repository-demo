namespace BlobStorageRepositoryDemo.Repository;

public interface IBlobStorageService
{
    Task DeleteDocumentAsync(string blobName);
    Task<Stream> GetBlobContentAsync(string blobName);
    Task UpdateBlobContentAsync(string blobName, Stream content);
    Task<List<string>?> GetListOfBlobsInFolderAsync(string folderName);
}