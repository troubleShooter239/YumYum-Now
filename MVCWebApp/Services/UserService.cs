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
    
    public async Task<User> Create(User user)
    {
        await _users.InsertOneAsync(user);
        return user;
    }

    public async Task<List<User>> GetAll() 
        => (await _users.FindAsync(user => true)).ToList();

    public async Task<User> Get(string id) 
        => (await _users.FindAsync(user => user.Id == id)).FirstOrDefault();
    
    public async void Remove(string id) 
        => await _users.DeleteOneAsync(user => user.Id == id);

    public async void Update(string id, User user)
        => await _users.ReplaceOneAsync(user => user.Id == id, user);
}
