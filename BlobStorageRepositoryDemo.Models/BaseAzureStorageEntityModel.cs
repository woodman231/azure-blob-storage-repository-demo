namespace BlobStorageRepositoryDemo.Models;

public class BaseAzureStorageEntityModel : IBaseAzureStorageEntityModel
{
    public BaseAzureStorageEntityModel()
    {
        this.Id = Guid.NewGuid().ToString();        
    }

    public BaseAzureStorageEntityModel(string id)
    {
        this.Id = id;        
    }

    public string Id {get; set;}
}
