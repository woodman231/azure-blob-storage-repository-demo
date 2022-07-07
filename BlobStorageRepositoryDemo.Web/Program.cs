using Azure.Identity;
using Microsoft.Extensions.Azure;
using BlobStorageRepositoryDemo.Repository;
using BlobStorageRepositoryDemo.Service;

var builder = WebApplication.CreateBuilder(args);

// Register Azure Clients
builder.Services.AddAzureClients(azureClientsBuilder => {
    azureClientsBuilder.AddBlobServiceClient(builder.Configuration.GetConnectionString("AzureStorage"));

    azureClientsBuilder.UseCredential(new DefaultAzureCredential());
});

// Register Repositories
builder.Services.AddTransient<ITodoAzureBlobStorageRepository, TodoAzureBlobStorageRepository>();

// Register Services
builder.Services.AddTransient<ITodoService, TodoService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
