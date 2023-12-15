using MVCWebApp.Models.User;

namespace MVCWebApp.Services;

public interface IUserService
{
    Task<List<User>> GetAll();
    Task<User> Get(string id);
    Task<User> Create(User user);
    void Update(string id, User user);
    void Remove(string id);
}
