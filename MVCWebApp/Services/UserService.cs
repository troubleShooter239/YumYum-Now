using MongoDB.Driver;
using MVCWebApp.Models.DbSettings;
using MVCWebApp.Models.User;

namespace MVCWebApp.Services;

public class UserService : IUserService
{
    private readonly IMongoCollection<User> _users;

    public UserService(IYumYumNowDbSettings settings, IMongoClient mongoClient)
        => _users = mongoClient.
        GetDatabase(settings.DatabaseName).
        GetCollection<User>(settings.UsersCollectionName);
    
    public async Task Create(User user)
        => await _users.InsertOneAsync(user);

    public async Task<List<User>> GetAll() 
        => (await _users.FindAsync(user => true)).ToList();

    public async Task<User> Get(string id) 
        => (await _users.FindAsync(user => user.Id == id)).FirstOrDefault();

    public async Task<User> GetByEmail(string email)
        => (await _users.FindAsync(user => user.Email == email)).FirstOrDefault();

    public async Task<User> GetByPhone(string phone)
        => (await _users.FindAsync(user => user.PhoneNumber == phone)).FirstOrDefault();

    public async Task Remove(string id) 
        => await _users.DeleteOneAsync(user => user.Id == id);

    public async Task Update(string id, User user)
        => await _users.ReplaceOneAsync(user => user.Id == id, user);
}
