using pa_product_api.Model;

namespace pa_product_api.Services;

public interface IUserService
{
    User Authenticate(string username, string password);
    User Register(User user);
    User GetUserById(int id);
}