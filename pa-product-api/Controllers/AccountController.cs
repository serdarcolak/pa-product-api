using Microsoft.AspNetCore.Mvc;
using pa_product_api.Model;

namespace pa_product_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{   
    //Sahte data
    private static List<Account> AccountList = new List<Account>
    {
        new Account{Id = 1, FirstName = "SERDAR",LastName = "ÇOLAK", AccountBalance = 500},
        new Account{Id = 2, FirstName = "TEST",LastName = "TEST", AccountBalance = 200},
        new Account{Id = 3, FirstName = "TEST1",LastName = "TEST1", AccountBalance = 100},
        new Account{Id = 4, FirstName = "AYŞE",LastName = "YILMAZ", AccountBalance = 800},
        new Account{Id = 5, FirstName = "AHMET",LastName = "ÇOLAK", AccountBalance = 600}
    };
    
    //Tüm Account verilerini çekme
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(AccountList);
    }
    
    //Account id'ye göre veriyi çekme
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var accounts = AccountList.FirstOrDefault(p => p.Id == id);

        if (accounts == null)
        {
            return NotFound();
        }
        
        return Ok(accounts);
    }
    
    //FromBody ile yeni bir Account oluşturma
    [HttpPost]
    public IActionResult Post([FromBody]Account account)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        account.Id = AccountList.Count + 1;
        AccountList.Add(account);
        return Ok(AccountList);
    }
    
    //Account güncelleme işlemi
    [HttpPut]
    public IActionResult Put(int id, Account account)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var accountResult = AccountList.FirstOrDefault(p => p.Id == id);
        if (accountResult == null)
        {
            return NotFound();
        }

        accountResult.FirstName = account.FirstName;
        accountResult.LastName = account.LastName;
        accountResult.AccountBalance = account.AccountBalance;

        return Ok(accountResult); 
    }
    
    //FromQuery ile route verilmeden delete işlemi yapılmaktadır.
    [HttpDelete]
    public IActionResult Delete([FromQuery] string id)
    {
        var accountResult = AccountList.FirstOrDefault(p => p.Id == Convert.ToInt32(id));
        if (accountResult == null)
        {
            return NotFound();
        }

        AccountList.Remove(accountResult);
        return NoContent();
    }
    
    //FirstName ve LastName göre listeleme işlemi
    [HttpGet("list")]
    public IActionResult List(string firstName = null, string lastName = null)
    {
        IQueryable<Account> query = AccountList.AsQueryable();

        if (!string.IsNullOrEmpty(firstName))
        {
            query = query.Where(p => p.FirstName.Contains(firstName));
        }
        else
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(lastName))
        {
            query = query.Where(p => p.LastName.Contains(lastName));
        }
        else
        {
            return NotFound();
        }
        
        return Ok(query.ToList());
    }
    
    
    //Alanlara göre sıralama işlemi
    [HttpGet("list/sort")]
    public IActionResult ListSorted(string sortBy = "firstname", bool descending = false)
    {
        IQueryable<Account> query = AccountList.AsQueryable();

        switch (sortBy.ToLower())
        {
            case "firstname":
                query = descending ? query.OrderByDescending(p => p.FirstName) : query.OrderBy(p => p.FirstName);
                break;
            case "lastname":
                query = descending ? query.OrderByDescending(p => p.LastName) : query.OrderBy(p => p.LastName);
                break;
            case "accountbalance":
                query = descending ? query.OrderByDescending(p => p.AccountBalance) : query.OrderBy(p => p.AccountBalance);
                break;
            default:
                return BadRequest("Invalid sort");
        }

        return Ok(query.ToList());
    }

}