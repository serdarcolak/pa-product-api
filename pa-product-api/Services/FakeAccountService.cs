using pa_product_api.Model;

namespace pa_product_api.Services;

public class FakeAccountService : IAccountService
{
    //Sahte data
    private  readonly List<Account> accounts = new List<Account>
    {
        new Account{Id = 1,FirstName = "SERDAR",LastName = "ÇOLAK", AccountBalance = 500},
        new Account{Id = 2,FirstName = "TEST",LastName = "TEST", AccountBalance = 200},
        new Account{Id = 3,FirstName = "TEST1",LastName = "TEST1", AccountBalance = 100},
        new Account{Id = 4,FirstName = "AYŞE",LastName = "YILMAZ", AccountBalance = 800},
        new Account{Id = 5,FirstName = "AHMET",LastName = "ÇOLAK", AccountBalance = 600}
    };

    
    public IEnumerable<Account> GetAllAccounts()
    {
        return accounts;
    }

    public Account GetAccountById(int id)
    {
        return accounts.FirstOrDefault(x => x.Id == id);
    }

    public void DeleteAccount(int id)
    {
        var account = accounts.FirstOrDefault(x => x.Id == id);
        accounts.Remove(account);
    }

    public void AddAccount(Account account)
    {
        accounts.Add(account);
    }

    public void UpdateAccount(Account account)
    {
        var accountResult = accounts.FirstOrDefault(x => x.Id == account.Id);

        if (accountResult != null)
        {
            accountResult.FirstName = account.FirstName != default ? account.FirstName : accountResult.FirstName;
            accountResult.LastName = account.LastName != default ? account.LastName : accountResult.LastName;
            accountResult.AccountBalance =
                account.AccountBalance != default ? account.AccountBalance : accountResult.AccountBalance;
        }
    }
}