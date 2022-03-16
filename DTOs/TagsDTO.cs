using System.Text.Json.Serialization;

namespace Online.DTOs;

public record TagsDTO
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

     [JsonPropertyName("brand")]
    public string Brand { get; set; }

    [JsonPropertyName("color")]
    public string Color { get; set; }

    [JsonPropertyName("model")]

    public string Model  { get; set; }
    
     [JsonPropertyName("product_id")]

    public long ProductId { get; set; }
}
public record TagsCreateDTO
{
    

   [JsonPropertyName("brand")]
    public string Brand { get; set; }

    [JsonPropertyName("color")]
    public string Color { get; set; }

    [JsonPropertyName("model")]

    public string Model  { get; set; }
    
     [JsonPropertyName("product_id")]

    public long ProductId { get; set; }
}