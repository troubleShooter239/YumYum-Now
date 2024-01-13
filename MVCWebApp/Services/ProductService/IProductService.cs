using MVCWebApp.Models.ProductDB;

namespace MVCWebApp.Services.ProductService;

/// <summary>
/// Interface for product-related operations.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Retrieves all products.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of products.</returns>
    Task<List<Product>> GetAll();

    /// <summary>
    /// Retrieves a product by ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the product.</returns>
    Task<Product> Get(string id);

    /// <summary>
    /// Retrieves a product by name.
    /// </summary>
    /// <param name="name">The name of the product to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the product.</returns>
    Task<Product> GetByName(string name);

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="product">The product to create.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Create(Product product);

    /// <summary>
    /// Updates a product by ID.
    /// </summary>
    /// <param name="id">The ID of the product to update.</param>
    /// <param name="product">The updated product data.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Update(string id, Product product);

    /// <summary>
    /// Removes a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to remove.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Remove(string id);
}
