using System.Text.Json.Serialization;
namespace LLGB.API.Models;

public class Student
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("Name")]
    public string Name { get; set; }
    [JsonPropertyName("Email")]
    public string Email { get; set; }
   
}
