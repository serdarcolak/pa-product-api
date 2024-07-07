namespace pa_product_api.Services;

public interface IUserService
{
    bool ValidateUser(string Username, string password);
}