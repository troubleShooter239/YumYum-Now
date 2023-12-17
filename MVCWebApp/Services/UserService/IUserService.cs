using MVCWebApp.Models.User;

namespace MVCWebApp.Services.UserService;

/// <summary>
/// Interface for user-related operations.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Retrieves all users.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of users.</returns>
    Task<List<User>> GetAll();

    /// <summary>
    /// Retrieves a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user.</returns>
    Task<User> Get(string id);

    /// <summary>
    /// Retrieves a user by email address.
    /// </summary>
    /// <param name="email">The email address of the user to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user.</returns>
    Task<User> GetByEmail(string email);

    /// <summary>
    /// Retrieves a user by phone number.
    /// </summary>
    /// <param name="phone">The phone number of the user to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user.</returns>
    Task<User> GetByPhone(string phone);

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The user to create.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Create(User user);

    /// <summary>
    /// Updates a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="user">The updated user data.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Update(string id, User user);

    /// <summary>
    /// Removes a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to remove.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Remove(string id);
}
