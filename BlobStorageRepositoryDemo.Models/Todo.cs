namespace BlobStorageRepositoryDemo.Models;

public class Todo : BaseAzureStorageEntityModel
{
    public string? Title {get; set;}
    public string? Description {get; set;}
    public bool Completed {get; set;} = false;    
}
