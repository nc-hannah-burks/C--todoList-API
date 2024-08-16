using System.Text.Json.Serialization;  //sets further requirements/rules for inputs 

namespace TodoApi.Models;

public class TodoCompleteCount
{

    public int CompleteCount { get; set; }
    public int UserId { get; set; } // Required foreign key property
    [JsonIgnore]
    public TodoUser TodoUser { get; set; } = null!; // Required reference navigation to principal
}
