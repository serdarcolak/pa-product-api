using System.ComponentModel.DataAnnotations;

namespace pa_product_api.Model;

public class Account
{   
    [Required(ErrorMessage = "You must log in to the ID field.")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "You must log in to the firstname field.")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "You must log in to the lastname field.")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "You must log in to the AccountBalance field.")]
    public decimal AccountBalance { get; set; }
}