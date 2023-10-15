using System.Text.Json.Serialization;
namespace MigrationTest.API.Models;

public class Student
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("grade")]
    public string Grade { get; set; }

}
