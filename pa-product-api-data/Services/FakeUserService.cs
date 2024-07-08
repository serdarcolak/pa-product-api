using pa_product_api.Model;

namespace pa_product_api.Services;

public class FakeUserService : IUserService
{
    private readonly List<User> _users = new List<User>
    {
        new User { Username = "user1", Password = "password1" },
        new User { Username = "user2", Password = "password2" }
    };

    public bool ValidateUser(string username, string password)
    {
        return _users.Any(u => u.Username == username && u.Password == password);
    }
}