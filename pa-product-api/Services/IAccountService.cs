using pa_product_api.Model;

namespace pa_product_api.Services;

public interface IAccountService
{
    IEnumerable<Account> GetAllAccounts();
    Account GetAccountById(int id);
    void DeleteAccount(int id);
    void AddAccount(Account account);
    void UpdateAccount(Account account);
    
}