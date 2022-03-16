using Online.DTOs;

namespace Online.Models;

public record Product
{
    public long Id  { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

 public ProductDTO asDto => new ProductDTO
    {
      Id = Id,
      Name = Name,
      Price = Price,
    };
}
