namespace pa_product_api_base.Entity;

public class BaseEntity
{
    public long Id { get; set; }
    public string InsertUser { get; set; }
    public DateTime InsertDate { get; set; }
    public bool IsActive { get; set; }
}