using System.Text.Json.Serialization;

namespace Online.DTOs;

public record OrderDTO
{
[JsonPropertyName("id")]
public long Id { get; set; }

 [JsonPropertyName("customer_id")]

 public long CustomerId { get; set; }

 [JsonPropertyName("ordered_at")]

public DateTimeOffset OrderedAt { get; set; }

 [JsonPropertyName("total_price")]

public decimal TotalPrice { get; set; }

 [JsonPropertyName("mode_of_payment")]

public string MdeOfPayment { get; set; }

 [JsonPropertyName("products")]

public List<ProductDTO> Product { get; set;}
}
public record OrderCreateDTO
{

 [JsonPropertyName("customer_id")]

 public long CustomerId { get; set; }

 [JsonPropertyName("ordered_at")]

public DateTimeOffset OrderedAt { get; set; }

 [JsonPropertyName("total_price")]

public decimal TotalPrice { get; set; }

 [JsonPropertyName("mode_of_payment")]

public string ModeOfPayment { get; set; }

 [JsonPropertyName("mode_of_payment")]

 public List<ProductDTO> Product {get; set;}


}