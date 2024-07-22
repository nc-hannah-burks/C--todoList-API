using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;   //sets further requirements/rules for inputs 
namespace TodoApi.Models;



public class TodoItemDTO
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public bool IsComplete { get; set; }


    public string? Details { get; set; }
    public int UserId { get; set; } // Required foreign key property
    [JsonIgnore]
    public TodoUser TodoUser { get; set; } = null!; // Required reference navigation to principal

}