using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;  //sets further requirements/rules for inputs 


namespace TodoApi.Models;

public class TodoItem
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
    public string? Secret { get; set; }
    // Foreign key for TodoUser
    [ForeignKey("TodoUser")]
    public long UserId { get; set; }

    // Navigation property
    public required TodoUser TodoUser { get; set; }
}