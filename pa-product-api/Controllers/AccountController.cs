using Microsoft.AspNetCore.Mvc;
using pa_product_api.Model;
using pa_product_api.Services;

namespace pa_product_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{   
    //service
    private readonly IAccountService accountService;
    
    
    public AccountController(IAccountService accountService)
    {
        this.accountService = accountService;
    }
    
    //Tüm Account verilerini çekme
    [HttpGet]
    public IActionResult GetAccounts()
    {
        var accounts = accountService.GetAllAccounts();
        return Ok(accounts);
    }
    
    //Account id'ye göre veriyi çekme
    [HttpGet("{id}")]
    public IActionResult GetAccount(int id)
    {
        var account = accountService.GetAccountById(id);

        if (account == null)
        {
            return NotFound();
        }
        
        return Ok(account);
    }
    
    //FromBody ile yeni bir Account oluşturma
    [HttpPost]
    public IActionResult CreateAccount([FromBody]Account account)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        //Modele DatabaseGenerated eklendi.
        //account.Id = AccountList.Count + 1;
        accountService.AddAccount(account);
        return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
    }
    
    //Account güncelleme işlemi.
    [HttpPut]
    public IActionResult UpdateAccount(int id, Account account)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var accountResult = accountService.GetAccountById(id);
        if (accountResult == null)
        {
            return NotFound();
        }
        accountService.UpdateAccount(accountResult);
        return Ok(accountResult);
        
    }
    
    //FromQuery ile route verilmeden delete işlemi yapılmaktadır.
    [HttpDelete]
    public IActionResult DeleteAccount([FromQuery] string id)
    {
        var accountResult = accountService.GetAccountById(Convert.ToInt32(id));
        if (accountResult == null)
        {
            return NotFound();
        }
        
        accountService.DeleteAccount(Convert.ToInt32(id));
        return NoContent();
    }
    
    //FirstName ve LastName göre listeleme işlemi
    [HttpGet("list")]
    public IActionResult ListAccounts(string firstName = null, string lastName = null)
    {
        IQueryable<Account> query = accountService.GetAllAccounts().AsQueryable();

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
    public IActionResult ListAccountsSorted(string sortBy = "firstname", bool descending = false)
    {
        IQueryable<Account> query = accountService.GetAllAccounts().AsQueryable();

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