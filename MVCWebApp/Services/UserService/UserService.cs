using MongoDB.Driver;
using MVCWebApp.Models.DbSettings;
using MVCWebApp.Models.User;

namespace MVCWebApp.Services.UserService;

/// <summary>
/// Implementation of IUserService using MongoDB for user data storage.
/// </summary>
public class UserService : IUserService
{
    private readonly IMongoCollection<User> _users;

    /// <summary>
    /// Initializes a new instance of the UserService class.
    /// </summary>
    /// <param name="settings">The database settings.</param>
    /// <param name="mongoClient">The MongoDB client.</param>
    public UserService(IYumYumNowDbSettings settings, IMongoClient mongoClient)
        => _users = mongoClient
            .GetDatabase(settings.DatabaseName)
            .GetCollection<User>(settings.UsersCollectionName);

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The user to create.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Create(User user)
        => await _users.InsertOneAsync(user);

    /// <summary>
    /// Retrieves all users.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of users.</returns>
    public async Task<List<User>> GetAll()
        => (await _users.FindAsync(user => true)).ToList();

    /// <summary>
    /// Retrieves a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user.</returns>
    public async Task<User> Get(string id)
        => (await _users.FindAsync(user => user.Id == id)).FirstOrDefault();

    /// <summary>
    /// Retrieves a user by email address.
    /// </summary>
    /// <param name="email">The email address of the user to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user.</returns>
    public async Task<User> GetByEmail(string email)
        => (await _users.FindAsync(user => user.Email == email)).FirstOrDefault();

    /// <summary>
    /// Retrieves a user by phone number.
    /// </summary>
    /// <param name="phone">The phone number of the user to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user.</returns>
    public async Task<User> GetByPhone(string phone)
        => (await _users.FindAsync(user => user.PhoneNumber == phone)).FirstOrDefault();

    /// <summary>
    /// Removes a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to remove.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Remove(string id)
        => await _users.DeleteOneAsync(user => user.Id == id);

    /// <summary>
    /// Updates a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="user">The updated user data.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Update(string id, User user)
        => await _users.ReplaceOneAsync(user => user.Id == id, user);
}