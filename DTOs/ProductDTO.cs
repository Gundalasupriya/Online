using System.Text.Json.Serialization;

namespace Online.DTOs;

public record ProductDTO
{
    [JsonPropertyName("id")]

    public long Id  { get; set; }

    [JsonPropertyName("name")]

    public string Name { get; set; }
    
    [JsonPropertyName("price")]

    public decimal Price { get; set; }

     [JsonPropertyName("tags")]

     public List<TagsDTO> Tags {get; set;}

  }
  public record ProductCreatetDTO
  
  {
    

    [JsonPropertyName("name")]

    public string Name { get; set; }
    
    [JsonPropertyName("price")]

    public decimal Price { get; set; }

   
}