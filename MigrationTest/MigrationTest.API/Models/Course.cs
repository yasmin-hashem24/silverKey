using System.Text.Json.Serialization;
namespace MigrationTest.API.Models;

public class Course
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("instructor")]
    public string Instructor { get; set; }


}
