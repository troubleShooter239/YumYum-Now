using MongoDB.Driver;
using MVCWebApp.Models.DbSettings;
using MVCWebApp.Models.ProductDB;

namespace MVCWebApp.Services.ProductService;

public class ProductService : IProductService
{
    private readonly IMongoCollection<Product> _products;

    /// <summary>
    /// Initializes a new instance of the productservice class.
    /// </summary>
    /// <param name="settings">The database settings.</param>
    /// <param name="mongoClient">The MongoDB client.</param>
    public ProductService(IYumYumNowDbSettings settings, IMongoClient mongoClient)
        => _products = mongoClient
            .GetDatabase(settings.DatabaseName)
            .GetCollection<Product>(settings.ProductsCollectionName);
    
    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="product">The product to create.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Create(Product product)
        => await _products.InsertOneAsync(product);

    /// <summary>
    /// Retrieves a product by ID.
    /// </summary>
    /// <param name="id">The ID of the product to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the product.</returns>
    public async Task<Product> Get(string id)
        => (await _products.FindAsync(product => product.Id == id)).FirstOrDefault();

    /// <summary>
    /// Retrieves all products.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of products.</returns>
    public async Task<List<Product>> GetAll()
        => (await _products.FindAsync(u => true)).ToList();

    /// <summary>
    /// Retrieves a product by name.
    /// </summary>
    /// <param name="name">The name of the product to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the product.</returns>
    public async Task<Product> GetByName(string name)
        => (await _products.FindAsync(p => p.Name == name)).FirstOrDefault();

    /// <summary>
    /// Removes a product by ID.
    /// </summary>
    /// <param name="id">The ID of the product to remove.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Remove(string id)
        => await _products.DeleteOneAsync(p => p.Id == id);

    /// <summary>
    /// Updates a product by ID.
    /// </summary>
    /// <param name="id">The ID of the product to update.</param>
    /// <param name="product">The updated product data.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Update(string id, Product product)
        => await _products.ReplaceOneAsync(p => p.Id == id, product);
}
