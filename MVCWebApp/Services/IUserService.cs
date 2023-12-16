using MVCWebApp.Models.User;

namespace MVCWebApp.Services;

public interface IUserService
{
    Task<List<User>> GetAll();
    Task<User> Get(string id);
    Task<User> GetByEmail(string email);
    Task<User> GetByPhone(string phone);
    Task Create(User user);
    Task Update(string id, User user);
    Task Remove(string id);
}
