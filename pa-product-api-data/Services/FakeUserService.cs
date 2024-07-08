using pa_product_api.Model;

namespace pa_product_api.Services;

public class FakeUserService : IUserService
{
    private readonly List<User> _users = new List<User>();

    public User Authenticate(string username, string password)
    {
        return _users.SingleOrDefault(x => x.Username == username && x.Password == password);
    }

    public User Register(User user)
    {
        user.Id = _users.Count + 1;
        _users.Add(user);
        return user;
    }

    public User GetUserById(int id)
    {
        return _users.SingleOrDefault(x => x.Id == id);
    }
}